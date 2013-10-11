//
//  LoginViewController.h
//  GpsTracker
//
//  Created by Adam Nagy on 30/09/2013.
//  Copyright (c) 2013 Adam Nagy. All rights reserved.
//

#import <UIKit/UIKit.h>

@interface LoginViewController : UIViewController
<UIWebViewDelegate, UIGestureRecognizerDelegate>

+ (NSString*)getUserId;
+ (Boolean)loggedIn;
+ (NSString*)getAccessToken;
+ (NSString*)getAccessTokenSecret;
+ (void)logInIfNeeded:(void (^)(void))OnSucessHandler
currentViewController:(UIViewController*)CurrentViewController;
+ (void)logOut:(void (^)(void))OnSucessHandler;

@property (weak, nonatomic) IBOutlet UIWebView *webView;

@end
