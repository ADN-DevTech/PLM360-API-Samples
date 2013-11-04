//
//  LoginViewController.m
//  GpsTracker
//
//  Created by Adam Nagy on 30/09/2013.
//  Copyright (c) 2013 Adam Nagy. All rights reserved.
//

#import "LoginViewController.h"
#import "UserSettings.h"
#import "Msg.h"

// This is an OAuth library I got from
// https://github.com/tweetdeck/TDOAuth
// The only thing I needed to modify was the
// function "URLRequestForPath/POSTParameters" so that I could
// add "oauth_session_handle" to the "Authorization" header of
// the POST message
#import "TDOAuth.h"

@interface LoginViewController ()

@end

@implementation LoginViewController

static void (^onSuccess)(void);
static LoginViewController *instance;

static NSString *o2UserId;
static NSString *accessToken;
static NSString *accessTokenSecret;
static NSString *sessionHandle;

NSString *requestToken;
NSString *requestTokenSecret;
NSString *accessTokenExpires;
NSString *authorizationExpires;

/// This function gets the URL parameters from the string
- (NSMutableDictionary *)getParams:(NSString *)fromString
{
  NSMutableDictionary *params = [NSMutableDictionary dictionary];
  NSArray *split = [fromString componentsSeparatedByString:@"&"];
  for (NSString *str in split)
  {
    NSArray *split2 = [str componentsSeparatedByString:@"="];
    if (split.count > 1)
      [params setObject:split2[1] forKey:split2[0]];
  }
  
  return params;
}

/// Convert the seconds to days/hours/minutes
- (NSString *)convertToDate:(NSString *)seconds
{
  double secs = [seconds doubleValue] / 1000.0;
  double days = floor(secs / 86400);
  secs -= days * 86400;
  double hours = floor(secs / 3600);
  secs -= hours * 3600;
  double minutes = floor(secs / 60);
  
  return [NSString
          stringWithFormat:@"%.0f days, %.0f hours, %.0f minutes",
          days, hours, minutes];
}

/// The first step of authentication is to request a token
- (Boolean)RequestToken
{
  NSMutableDictionary * dict = nil;
  
  NSURLRequest * req =
  [TDOAuth
   URLRequestForPath:@"/OAuth/RequestToken"
   POSTParameters:nil
   extraParams:dict
   host:O2_HOST
   consumerKey:O2_OAUTH_KEY
   consumerSecret:O2_OAUTH_SECRET
   accessToken:nil
   tokenSecret:nil];
  
  NSURLResponse * response;
  NSError * error = nil;
  NSData * result = [NSURLConnection
                     sendSynchronousRequest:req
                     returningResponse:&response
                     error:&error];
  
  NSString * s = [[NSString alloc]
                  initWithData:result
                  encoding:NSUTF8StringEncoding];
  
  // Let's get the tokens we need
  NSMutableDictionary * params = [self getParams:s];
  requestToken = params[@"oauth_token"];
  requestTokenSecret = params[@"oauth_token_secret"];
  
  // If we did not get those params then something went wrong
  if (requestToken == nil)
  {
    [Msg inform:@"Could not get request token!"];
    
    return false;
  }
  
  return true;
}

/// The second step is to authorize the user using the
/// Autodesk login system
- (void)Authorize
{
  NSString * path =
  [NSString
   stringWithFormat:@"%@?oauth_token=%@&viewmode=mobile",
   O2_AUTHORIZE,
   requestToken];
  
  // Otherwise let's load the page in our web viewer so that
  // we can catch the URL that it gets redirected to
  NSURLRequest * req =
  [NSURLRequest
   requestWithURL:[NSURL URLWithString:path]
   cachePolicy:NSURLRequestUseProtocolCachePolicy
   timeoutInterval:TDOAuthURLRequestTimeout];
  
  [self.webView loadRequest:req];
}

/// The third step is to authenticate using the request tokens
/// Once you get the access token and access token secret
/// you need to use those to make your further REST calls
/// Same in case of refreshing the access tokens or invalidating
/// the current session. To do that we need to pass in
/// the acccess token and access token secret as the accessToken and
/// tokenSecret parameter of the [TDOAuth URLRequestForPath] function
- (void)AccessToken:(Boolean)refresh PIN:(NSString *)PIN
{
  NSString * tokenParam = requestToken;
  NSString * tokenSecretParam = requestTokenSecret;
  NSMutableDictionary * dict = [NSMutableDictionary dictionary];
  
  // If we already got access tokens and now just try to refresh
  // them then we need to provide the session handle
  if (refresh)
  {
    [dict setObject:sessionHandle forKey:@"oauth_session_handle"];
    tokenParam = accessToken;
    tokenSecretParam = accessTokenSecret;
  }
  
  NSURLRequest * req =
  [TDOAuth
   URLRequestForPath:@"/OAuth/AccessToken"
   POSTParameters:nil
   extraParams:dict
   host:O2_HOST
   consumerKey:O2_OAUTH_KEY
   consumerSecret:O2_OAUTH_SECRET
   accessToken:tokenParam
   tokenSecret:tokenSecretParam];
  
  NSURLResponse * response;
  NSError * error = nil;
  NSData * result = [NSURLConnection
                     sendSynchronousRequest:req
                     returningResponse:&response
                     error:&error];
  
  NSString * s = [[NSString alloc]
                  initWithData:result
                  encoding:NSUTF8StringEncoding];
  
  // Let's get the tokens we need
  NSMutableDictionary * params = [self getParams:s];
  accessToken = params[@"oauth_token"];
  accessTokenSecret = params[@"oauth_token_secret"];
  sessionHandle = params[@"oauth_session_handle"];
  accessTokenExpires = params[@"oauth_expires_in"];
  authorizationExpires = params[@"oauth_authorization_expires_in"];
  o2UserId = params[@"x_oauth_user_name"];
  // user id needs to be URL undecoded
  o2UserId = [o2UserId stringByReplacingPercentEscapesUsingEncoding:NSUTF8StringEncoding];
  
  // If session handle is not null then we got the tokens
  if (sessionHandle != nil)
  {
    if (refresh)
      [Msg inform:@"Token refresh successful!"];
    else
      [Msg inform:@"Log in successful!"];
    
    // We can dismiss this view then
    [self dismissViewControllerAnimated:false completion:onSuccess];
  }
  else
  {
    if (refresh)
      [Msg inform:@"Could not refresh token!"];
    else
      [Msg inform:@"Could not get access token!"];
  }
}

/// If we do not want to use the service anymore then
/// the best thing is to log out, i.e. invalidate the tokens we got
+ (void)InvalidateToken
{
  NSURLRequest * req =
  [TDOAuth
   URLRequestForPath:@"/OAuth/InvalidateToken"
   POSTParameters:nil
   extraParams:[NSMutableDictionary
   dictionaryWithObject:sessionHandle
   forKey:@"oauth_session_handle"]
   host:O2_HOST
   consumerKey:O2_OAUTH_KEY
   consumerSecret:O2_OAUTH_SECRET
   accessToken:accessToken
   tokenSecret:accessTokenSecret];
  
  NSURLResponse * response;
  NSError * error = nil;
  NSData * result = [NSURLConnection
                     sendSynchronousRequest:req
                     returningResponse:&response
                     error:&error];
  NSString * s = [[NSString alloc]
                  initWithData:result
                  encoding:NSUTF8StringEncoding];
  
  // If Invalidate was successful, we will not get back any data
  if ([s isEqualToString:@""])
  {
    // Clear the various tokens
    requestToken = requestTokenSecret =
    o2UserId = sessionHandle =
    accessToken = accessTokenSecret = nil;
    
    onSuccess();
  }
  else
  {
    [Msg inform:@"Could not log out!"];
  }
}

// Monitor where we are sent and reject it if not good
- (BOOL)webView:(UIWebView *)webView
shouldStartLoadWithRequest:(NSURLRequest *)request
navigationType:(UIWebViewNavigationType)navigationType
{
  return true;
}

- (void)webViewDidStartLoad:(UIWebView *)webView
{
}

/// When a new URL is being shown in the browser
/// then we can check the URL
/// This is needed in case of in-band authorization
/// which will redirect us to a given
/// URL (O2_ALLOW) in case of success
- (void)webViewDidFinishLoad:(UIWebView *)aWebView
{
  // Let's check if we got redirected to the correct page
  if ([self isAuthorizeCallBack])
  {
    [self AccessToken:false PIN:nil];
  }
}

/// Check if the URL is O2_ALLOW, which means that
/// the user could log in successfully
- (Boolean)isAuthorizeCallBack
{
  NSString * fullUrlString = self.webView.request.URL.absoluteString;
  
  if (!fullUrlString)
    return false;
  
  NSArray * arr = [fullUrlString componentsSeparatedByString:@"?"];
  if (!arr || arr.count!=2)
    return false;
  
  // If we were redirected to the O2_ALLOW URL
  // then the user could log in successfully
  if ([arr[0] isEqualToString:O2_ALLOW])
    return true;
  
  // If we got to this page then
  // probably there is an issue
  if ([arr[0] isEqualToString:O2_AUTHORIZE])
  {
    [Msg
    ask:@"Could not log in!\nTry again? "
    option1:@"No"
    option2:@"Yes"
    handler1:
    ^{
      [self dismissViewControllerAnimated:true completion:nil];
    }
    handler2:
    ^{
      [self logIn];
    }];
  }
  
  return false;
}

/// Once the application's view got loaded
- (void)viewDidLoad
{
  instance = self;

  [super viewDidLoad];

  [self logIn];
}

- (void)logIn
{
  // Let's log in
  // Test the oxygen system
  
  // Step 1
  if ([self RequestToken])
  {
    // Step 2
    [self Authorize];
    
    // Step 3
    // If Authorize succeeds, then in case of out-of-band authorization
    // the /OAuth/AccessToken will be called from
    // didDismissWithButtonIndex, but in case of in-band authorization
    // it will be called from webViewDidFinishLoad
  }
}

- (void)didReceiveMemoryWarning
{
  [super didReceiveMemoryWarning];
  
  // Dispose of any resources that can be recreated.
}

//////////////////////////////////////////////////////////////////////
// Interface /////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////

+ (Boolean)loggedIn
{
  return (o2UserId != nil);
}

+ (NSString*)getUserId
{
  return o2UserId;
}

+(NSString*)getAccessToken
{
  return accessToken;
}

+(NSString*)getAccessTokenSecret
{
  return accessTokenSecret;
}

+(void)logInIfNeeded:(void (^)(void))OnSucessHandler
currentViewController:(UIViewController*)CurrentViewController
{
  onSuccess = OnSucessHandler;
  
  if ([self loggedIn])
    onSuccess();
  else
    [CurrentViewController performSegueWithIdentifier:@"showLogin" sender:CurrentViewController];
}

+ (void)logOut:(void (^)(void))OnSucessHandler
{
  onSuccess = OnSucessHandler;
  
  [LoginViewController InvalidateToken];
}

@end
