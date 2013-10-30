//
//  PlmPayloads.h
//  GpsTracker
//
//  Created by Adam Nagy on 04/10/2013.
//  Copyright (c) 2013 Adam Nagy. All rights reserved.
//

#import <Foundation/Foundation.h>

// You can create the below classes by querying things in the browser
// e.g. https://adsknagyad.autodeskplm360.net/api/v2/workspaces/54/items/3412
// then using this tool to generate classes for the json string
// http://jsonpack.com/ModelGenerators/ObjectiveC

//////////////////////////////////////////////////////////////////////
// PlmItem
//////////////////////////////////////////////////////////////////////

@interface PlmFields : NSObject
{
  NSString *NAME;
  NSString *GPS;
  NSDate *DATE;
  // DATE2 is just a string representation of the date
  // that can be used directly in the UI
  NSString *DATE2;
}

@property (nonatomic, retain) NSString *NAME;
@property (nonatomic, retain) NSString *GPS;
@property (nonatomic, retain) NSDate *DATE;
@property (nonatomic, retain) NSString *DATE2;

+ (PlmFields*) objectFromJson:(NSDictionary*)dict;

+ (PlmFields*)
objectWithNAME:(NSString*)NAME
GPS:(NSString*)GPS
DATE:(NSDate*)DATE;

- (NSDictionary*)jsonFromObject;

@end

@interface PlmItem : NSObject
{
  NSNumber *Id;
  PlmFields *Fields;
}

@property (nonatomic, retain) NSNumber *Id;
@property (nonatomic, retain) PlmFields *Fields;

+ (NSData*)
dataWithNAME:(NSString*)NAME
GPS:(NSString*)GPS
DATE:(NSDate*)DATE;

+ (NSDictionary*)
jsonWithNAME:(NSString*)NAME
GPS:(NSString*)GPS
DATE:(NSDate*)DATE;

+ (PlmItem*) objectFromJson:(NSDictionary*)dict;

@end

//////////////////////////////////////////////////////////////////////
// PlmPage
//////////////////////////////////////////////////////////////////////

@interface PlmPage : NSObject
{
  NSNumber *Index;
  NSNumber *Size;
  NSURL *NextUrl;
  NSURL *PrevUrl;
}

@property (nonatomic, retain) NSNumber *Index;
@property (nonatomic, retain) NSNumber *Size;
@property (nonatomic, retain) NSURL *NextUrl;
@property (nonatomic, retain) NSURL *PrevUrl;

+ (PlmPage*) objectFromJson:(NSDictionary*)dict;

@end

//////////////////////////////////////////////////////////////////////
// PlmItems
//////////////////////////////////////////////////////////////////////

@interface PlmItems : NSObject
{
  PlmPage *Page;
  NSMutableArray *Elements;
}

@property (nonatomic, retain) PlmPage *Page;
@property (nonatomic, retain) NSMutableArray *Elements;

+ (PlmItems*) objectFromJson:(NSDictionary*)dict;

@end

//////////////////////////////////////////////////////////////////////
// PlmSession
//////////////////////////////////////////////////////////////////////

@interface PlmSession : NSObject
{
  NSString *UserId;
  NSString *CustomerToken;
}

@property (nonatomic, retain) NSString *UserId;
@property (nonatomic, retain) NSString *CustomerToken;

+ (PlmSession*) objectFromJson:(NSDictionary*)dict;

@end

//////////////////////////////////////////////////////////////////////
// PlmFile
//////////////////////////////////////////////////////////////////////

@interface PlmFile : NSObject
{
  NSNumber *Id;
  NSString *FileName;
  NSString *Title;
  NSString *Description;
}

@property (nonatomic, retain) NSNumber *Id;
@property (nonatomic, retain) NSString *FileName;
@property (nonatomic, retain) NSString *Title;
@property (nonatomic, retain) NSString *Description;

+ (NSData*)
dataWithFileName:(NSString*)FileName
Title:(NSString*)Title
Description:(NSString*)Description;

+ (PlmFile*) objectFromJson:(NSDictionary*)dict;

- (NSDictionary*)jsonFromObject;

@end

//////////////////////////////////////////////////////////////////////
// PlmFiles
//////////////////////////////////////////////////////////////////////

@interface PlmFiles : NSObject
{
  PlmPage *Page;
  NSMutableArray *Elements;
}

@property (nonatomic, retain) PlmPage *Page;
@property (nonatomic, retain) NSMutableArray *Elements;

+ (PlmFiles*) objectFromJson:(NSDictionary*)dict;

@end