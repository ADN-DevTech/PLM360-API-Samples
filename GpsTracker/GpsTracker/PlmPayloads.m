//
//  PlmPayloads.m
//  GpsTracker
//
//  Created by Adam Nagy on 04/10/2013.
//  Copyright (c) 2013 Adam Nagy. All rights reserved.
//

#import "PlmPayloads.h"

//////////////////////////////////////////////////////////////////////
// PlmItem / PlmFields
//////////////////////////////////////////////////////////////////////

#define PLM_DATE_FORMAT @"yyyy-MM-dd"

@implementation PlmFields

@synthesize NAME, GPS, DATE, DATE2;

+ (PlmFields*) objectFromJson:(NSDictionary*)dict
{
  NSDateFormatter *df = [[NSDateFormatter alloc] init];
    [df setDateFormat:PLM_DATE_FORMAT];

  PlmFields *plmFields = [PlmFields alloc];
  
  plmFields.NAME = [dict objectForKey:@"NAME"];
  plmFields.GPS = [dict objectForKey:@"GPS"];
  plmFields.DATE2 = [dict objectForKey:@"DATE"];
  plmFields.DATE = [df dateFromString:plmFields.DATE2];
  
  return plmFields;
}

+ (PlmFields*)
objectWithNAME:(NSString*)NAME
GPS:(NSString*)GPS
DATE:(NSDate*)DATE
{
  PlmFields *plmFields = [PlmFields alloc];
  plmFields.NAME = NAME;
  plmFields.GPS = GPS;
  plmFields.DATE = DATE;
  
  return plmFields;
}

- (NSDictionary*)jsonFromObject
{
  NSMutableDictionary *dict = [NSMutableDictionary dictionary];
  NSDateFormatter *df = [[NSDateFormatter alloc] init];
  [df setDateFormat:PLM_DATE_FORMAT];

  [dict setObject:self.NAME forKey:@"NAME"];
  [dict setObject:self.GPS forKey:@"GPS"];
  [dict setObject:[df stringFromDate:self.DATE] forKey:@"DATE"];
  
  return dict;
}

@end

@implementation PlmItem

@synthesize Id, Fields;

+ (NSData*)
dataWithNAME:(NSString*)NAME
GPS:(NSString*)GPS
DATE:(NSDate*)DATE
{
  NSDictionary *dict = [PlmItem jsonWithNAME:NAME GPS:GPS DATE:DATE];

  NSError * error = nil;
  
  return [NSJSONSerialization
          dataWithJSONObject:dict
          options:0 // no NSJSONWritingPrettyPrinted
          error:&error];  
}

+ (NSDictionary*)
jsonWithNAME:(NSString*)NAME
GPS:(NSString*)GPS
DATE:(NSDate*)DATE
{
  PlmItem *plmItem = [PlmItem alloc];
  plmItem.Fields = [PlmFields objectWithNAME:NAME GPS:GPS DATE:DATE];

  NSDictionary *dict = [plmItem jsonFromObject];

  return dict;
}

+ (PlmItem*) objectFromJson:(NSDictionary*)dict
{
  PlmItem *plmItem = [PlmItem alloc];
  
  plmItem.Id = [dict objectForKey:@"id"];
  plmItem.Fields = [PlmFields objectFromJson:[dict objectForKey:@"fields"]];
  
  return plmItem;
}

- (NSDictionary*)jsonFromObject
{
  NSMutableDictionary *dict = [NSMutableDictionary dictionary];
  
  [dict setObject:[Fields jsonFromObject] forKey:@"fields"];
  
  return dict;
}

@end

//////////////////////////////////////////////////////////////////////
// PlmPage
//////////////////////////////////////////////////////////////////////

@implementation PlmPage : NSObject

@synthesize Index, Size, NextUrl, PrevUrl;

+ (PlmPage*) objectFromJson:(NSDictionary*)dict
{
  PlmPage *plmPage = [PlmPage alloc];
  
  if (dict != (id)[NSNull null])
  {
    plmPage.Index = [dict objectForKey:@"index"];
    plmPage.Size = [dict objectForKey:@"size"];
    NSString *nextUrl = [dict objectForKey:@"nextUrl"];
    if (nextUrl != (id)[NSNull null])
      plmPage.NextUrl = [NSURL URLWithString:nextUrl];
    NSString *prevUrl = [dict objectForKey:@"prevUrl"];
    if (prevUrl != (id)[NSNull null])
      plmPage.PrevUrl = [NSURL URLWithString:prevUrl];
  }
  
  return plmPage;
}

@end

//////////////////////////////////////////////////////////////////////
// PlmItems
//////////////////////////////////////////////////////////////////////

@implementation PlmItems : NSObject

@synthesize Page, Elements;

+ (PlmItems*) objectFromJson:(NSDictionary*)dict
{
  PlmItems *plmItems = [PlmItems alloc];
  
  plmItems.Page = [PlmPage objectFromJson:[dict objectForKey:@"page"]];
  plmItems.Elements = [NSMutableArray array];
  NSArray *elems = [dict objectForKey:@"elements"];
  for (NSDictionary *item in elems)
  {
    [plmItems.Elements addObject:[PlmItem objectFromJson:item]];
  }
  
  return plmItems;
}

@end

//////////////////////////////////////////////////////////////////////
// PlmSession
//////////////////////////////////////////////////////////////////////

@implementation PlmSession

@synthesize UserId, CustomerToken;

+ (PlmSession*) objectFromJson:(NSDictionary*)dict
{
  PlmSession *plmSession = [PlmSession alloc];
  
  plmSession.UserId = [dict objectForKey:@"userId"];
  plmSession.CustomerToken = [dict objectForKey:@"customerToken"];
  
  return plmSession;
}

@end

//////////////////////////////////////////////////////////////////////
// PlmFile
//////////////////////////////////////////////////////////////////////

@implementation PlmFile

@synthesize Id, FileName, Title, Description;

+ (NSData*)
dataWithFileName:(NSString*)FileName
Title:(NSString*)Title
Description:(NSString*)Description
{
  PlmFile *plmFile = [PlmFile alloc];
  
  plmFile.FileName = FileName;
  plmFile.Title = Title;
  plmFile.Description = Description;
  
  NSDictionary *dict = [plmFile jsonFromObject];

  NSError * error = nil;
  
  return [NSJSONSerialization
          dataWithJSONObject:dict
          options:0 // no NSJSONWritingPrettyPrinted
          error:&error];
}

+ (PlmFile*) objectFromJson:(NSDictionary*)dict
{
  PlmFile *plmFile = [PlmFile alloc];
  
  plmFile.Id = [dict objectForKey:@"id"];
  plmFile.FileName = [dict objectForKey:@"fileName"];
  plmFile.Title = [dict objectForKey:@"title"];
  plmFile.Description = [dict objectForKey:@"description"];

  return plmFile;
}

- (NSDictionary*)jsonFromObject
{
  NSMutableDictionary *dict = [NSMutableDictionary dictionary];
  
  [dict setObject:FileName forKey:@"fileName"];
  [dict setObject:Title forKey:@"title"];
  [dict setObject:Description forKey:@"description"];
  
  return dict;
}

@end

//////////////////////////////////////////////////////////////////////
// PlmFiles
//////////////////////////////////////////////////////////////////////

@implementation PlmFiles : NSObject

@synthesize Page, Elements;

+ (PlmFiles*) objectFromJson:(NSDictionary*)dict
{
  PlmFiles *plmFiles = [PlmFiles alloc];
  
  plmFiles.Page = [PlmPage objectFromJson:[dict objectForKey:@"page"]];
  plmFiles.Elements = [NSMutableArray array];
  NSArray *elems = [dict objectForKey:@"elements"];
  for (NSDictionary *item in elems)
  {
    [plmFiles.Elements addObject:[PlmFile objectFromJson:item]];
  }
  
  return plmFiles;
}

@end


