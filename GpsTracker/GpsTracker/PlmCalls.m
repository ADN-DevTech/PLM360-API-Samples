//
//  RestCalls.m
//  GpsTracker
//
//  Created by Adam Nagy on 30/09/2013.
//  Copyright (c) 2013 Adam Nagy. All rights reserved.
//

#import "PlmCalls.h"
#import "Settings.h"
#import "PlmPayloads.h"

// Various paths for the quesries

#define PLM_PATH_LOGIN  @"/api/v2/authentication/oxygen-login"
#define PLM_PATH_LOGOUT @"/api/v2/authentication/logout"
#define PLM_PATH_ITEMS  @"/api/v2/workspaces/%@/items"
#define PLM_PATH_ITEM   @"/api/v2/workspaces/%@/items/%@"
#define PLM_PATH_FILES  @"/api/v2/workspaces/%@/items/%@/files"
#define PLM_PATH_FILE   @"/api/v2/workspaces/%@/items/%@/files/%@"

// Keeping track of the plm user's is
static NSString *plmUserId;

@interface PlmCalls ()

@end

@implementation PlmCalls

//////////////////////////////////////////////////////////////////////
// Internal functions ////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////

+ (NSDictionary*)httpPost:(NSString*)path
{
  NSURLRequest * req =
  [TDOAuth
   PostRequestForPath:path
   host:PLM_TENANT_URL
   data:nil
   consumerKey:O2_OAUTH_KEY
   consumerSecret:O2_OAUTH_SECRET
   accessToken:[LoginViewController getAccessToken]
   tokenSecret:[LoginViewController getAccessTokenSecret]
   multipart:false];
  
  NSURLResponse * response;
  NSError * error = nil;
  NSData * result = [NSURLConnection
                     sendSynchronousRequest:req
                     returningResponse:&response
                     error:&error];
  
  if (error)
    return nil;
  
  NSDictionary * json = [NSJSONSerialization
                         JSONObjectWithData:result
                         options:NSJSONReadingMutableContainers
                         error:&error];
  
  
#ifdef DEBUG_STRINGS
  NSString * s = [[NSString alloc]
                  initWithData:result
                  encoding:NSUTF8StringEncoding];
  NSLog(@"Response string: %@", s);
#endif
  
  return json;
}

+ (NSDictionary*)httpPost:(NSString*)path withData:(NSData*)data multipart:(Boolean)multipart
{
  NSURLRequest * req =
  [TDOAuth
   PostRequestForPath:path
   host:PLM_TENANT_URL
   data:data
   consumerKey:O2_OAUTH_KEY
   consumerSecret:O2_OAUTH_SECRET
   accessToken:[LoginViewController getAccessToken]
   tokenSecret:[LoginViewController getAccessTokenSecret]
   multipart:multipart];
  
  NSURLResponse * response;
  NSError * error = nil;
  NSData * result = [NSURLConnection
                     sendSynchronousRequest:req
                     returningResponse:&response
                     error:&error];
  
  if (error)
    return nil;
  
  NSDictionary * json = [NSJSONSerialization
                         JSONObjectWithData:result
                         options:NSJSONReadingMutableContainers
                         error:&error];
  
#ifdef DEBUG_STRINGS
  NSString * s = [[NSString alloc]
                  initWithData:result
                  encoding:NSUTF8StringEncoding];
  NSLog(@"Response string: %@", s);
#endif
  
  return json;
}

+ (NSDictionary*)httpGet:(NSString*)path headers:(NSDictionary*)headers
{
  NSURLRequest * req =
  [TDOAuth
   GetRequestForPath:path
   GETParameters:nil
   scheme:@"https"
   headers:headers
   host:PLM_TENANT_URL
   consumerKey:O2_OAUTH_KEY
   consumerSecret:O2_OAUTH_SECRET
   accessToken:[LoginViewController getAccessToken]
   tokenSecret:[LoginViewController getAccessTokenSecret]];
  
  NSURLResponse * response;
  NSError * error = nil;
  NSData * result = [NSURLConnection
                     sendSynchronousRequest:req
                     returningResponse:&response
                     error:&error];
  
  if (error)
    return nil;
  
  NSDictionary * json = [NSJSONSerialization
                         JSONObjectWithData:result
                         options:NSJSONReadingMutableContainers
                         error:&error];
  
#ifdef DEBUG_STRINGS
  NSString * s = [[NSString alloc]
                  initWithData:result
                  encoding:NSUTF8StringEncoding];
  NSLog(@"Response string: %@", s);
#endif
  
  return json;
}

+ (NSData*)httpGetData:(NSString*)path headers:(NSDictionary*)headers
{
  NSURLRequest * req =
  [TDOAuth
   GetRequestForPath:path
   GETParameters:nil
   scheme:@"https"
   headers:headers
   host:PLM_TENANT_URL
   consumerKey:O2_OAUTH_KEY
   consumerSecret:O2_OAUTH_SECRET
   accessToken:[LoginViewController getAccessToken]
   tokenSecret:[LoginViewController getAccessTokenSecret]];
  
  NSURLResponse * response;
  NSError * error = nil;
  NSData * result = [NSURLConnection
                     sendSynchronousRequest:req
                     returningResponse:&response
                     error:&error];
  
  return result;
}

+ (void)loginToPlm
{
  // Let's get the tokens we need
  PlmSession *plmSession =
  [PlmSession objectFromJson:
   [self httpPost:PLM_PATH_LOGIN]];
  plmUserId = plmSession.UserId;
}

+ (NSData*)assembleMultiPartData:(NSData*)data
name:(NSString*)name
contentType:(NSString*)contentType, ...
{
  NSMutableData *all = [NSMutableData data];
  
  ////////////////////////////////////////////////////////////////////
  /* Multipart Sample ////////////////////////////////////////////////
   POST http://myserver.com/QCCSvcHost/MIME/RealtimeTrans/ HTTP/1.1
   Content-Type: multipart/form-data; boundary="XbCY"
   Host: na-w-lxu3
   Content-Length: 1470
   Expect: 100-continue
   Connection: Keep-Alive
   
   --XbCY
   Content-Type: text/plain; charset=utf-8
   Content-Disposition: form-data; name=PayloadType
   
   X12_270_Request_005010X279A1
   --XbCY
   Content-Disposition: form-data; name=Payload; filename=276_5010.edi
   
   ISA*00*~SE*16*0001~GE*1*1~IEA*1*191543498~
   --XbCY--
  *///////////////////////////////////////////////////////////////////
  
  NSData *curData = data;
  NSString *curName = name;
  NSString *curContentType = contentType;
  
  va_list args;
  va_start(args, contentType);
  for (id arg = contentType; arg != nil; arg = va_arg(args, id))
  {
    // If this is not the first round
    if (arg != contentType)
    {
      curData = arg;
      arg = va_arg(args, id);
      curName = arg;
      arg = va_arg(args, id);
      curContentType = arg;
    }
    
    // Header
    [all appendData:
     [[NSString stringWithFormat:
       @"\r\n--%@\r\n"
       "Content-Type: %@\r\n"
       "Content-Disposition: form-data; name=\"%@\"\r\n\r\n",
       MULTIPART_BOUNDARY, curContentType, curName]
      dataUsingEncoding:NSUTF8StringEncoding]];
    
    // Data
    [all appendData:curData];
    
    // Ending
    [all appendData:
     [[NSString stringWithFormat:
       @"\r\n--%@", MULTIPART_BOUNDARY]
      dataUsingEncoding:NSUTF8StringEncoding]];
  }
  va_end(args);
  
  // Final ending
  [all appendData:
   [[NSString stringWithFormat:@"--\r\n"]
    dataUsingEncoding:NSUTF8StringEncoding]];
  
  return all;
}

//////////////////////////////////////////////////////////////////////
// Interface /////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////

+ (Boolean)storePosition:(NSString*)position
                withName:(NSString*)name
               withImage:(UIImage*)image
{
  if (!plmUserId)
    [self loginToPlm];
  
  NSData *data =
  [PlmItem
   dataWithNAME:name
   GPS:position
   DATE:[NSDate date]];
  
  NSString *path =
  [NSString stringWithFormat:PLM_PATH_ITEMS, PLM_WORKSPACE_ID];
  
  // Create new item
  PlmItem *plmItem =
  [PlmItem
   objectFromJson:
   [self httpPost:path withData:data multipart:false]];
  
  // If there is no image to attach, we have nothing more to do
  if (!image)
    return true;
  
  // First thing is the File information payload
  data =
  [PlmFile
   dataWithFileName:@"CameraShot.png"
   Title:@"CameraShot"
   Description:@"Camera Shot"];
  
  // Add the image to the multipart data
  NSData *body =
  [PlmCalls
    assembleMultiPartData:data
    name:@"payload"
    contentType:@"text/plain; charset=utf-8",
    UIImagePNGRepresentation(image),
    @"file",
    @"application/octet-stream",
    nil];
 
  // Get the path for the call
  path = [NSString stringWithFormat:PLM_PATH_FILES, PLM_WORKSPACE_ID,
          plmItem.Id];
  
  // Send the data to PLM 360
  NSDictionary *item = [self httpPost:path withData:body multipart:true];
  
  return (item != nil);
}

+ (NSDictionary*)getPositions
{
  if (!plmUserId)
    [self loginToPlm];
  
  NSMutableDictionary *positions = [[NSMutableDictionary alloc] init];
  
  NSString *path =
  [NSString stringWithFormat:PLM_PATH_ITEMS, PLM_WORKSPACE_ID];
  
  // For testing let's set page-size to 2
  // path = [path stringByAppendingString:@"?page-size=2"];
  
  while (true)
  {
    PlmItems *plmItems =
    [PlmItems objectFromJson:[self httpGet:path headers:nil]];
    
#ifdef DEBUG_STRINGS
    NSString *page = [plmItems.Page.NextUrl description];
    
    NSLog(@"Next page = %@", page);
#endif
    
    for (PlmItem *item in plmItems.Elements)
    {
      NSString *path =
      [NSString stringWithFormat:PLM_PATH_ITEM, PLM_WORKSPACE_ID, item.Id];
      
      PlmItem *itemData =
      [PlmItem objectFromJson:[self httpGet:path headers:nil]];
      
      NSArray *data =
      [NSArray arrayWithObjects:
       itemData.Fields.GPS,
       itemData.Fields.NAME,
       itemData.Fields.DATE2, nil];
      
      [positions setObject:data forKey:item.Id];
    }
    
    // If there are no more pages then we're finished
    if (!plmItems.Page.NextUrl)
      break;
    
    path = [NSString stringWithFormat:@"%@?%@",
    [plmItems.Page.NextUrl path], [plmItems.Page.NextUrl query]];
  }
  
  return positions;
}

+ (UIImage*)getImage:(NSNumber*)itemId
{
  NSString *path =
  [NSString
   stringWithFormat:PLM_PATH_FILES,
   PLM_WORKSPACE_ID, itemId];
  
  PlmFiles *plmFiles =
  [PlmFiles objectFromJson:[self httpGet:path headers:nil]];
  
  if (plmFiles.Elements.count < 1)
    return nil;
  
  // Get the first file in the list
  PlmFile *plmFile = [plmFiles.Elements objectAtIndex:0];
  
  path =
  [NSString
   stringWithFormat:PLM_PATH_FILE,
   PLM_WORKSPACE_ID, itemId, plmFile.Id];
  
  NSDictionary *dict =
  [NSDictionary
   dictionaryWithObject:@"application/octet-stream"
   forKey:@"Accept"];
  
  NSData *file = [self httpGetData:path headers:dict];
  
  if (file)
    return [UIImage imageWithData:file];
  else
    return nil;
}

+ (Boolean)logOut
{
  // If we are not logged in there is nothing to do
  if (!plmUserId)
    return true;
  
  NSDictionary *json = [self httpPost:PLM_PATH_LOGOUT];
  
  // If successful
  if (json != nil)
  {
    plmUserId = nil;
    return true;
  }
  
  return false;
}

@end
