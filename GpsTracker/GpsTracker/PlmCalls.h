//
//  RestCalls.h
//  GpsTracker
//
//  Created by Adam Nagy on 30/09/2013.
//  Copyright (c) 2013 Adam Nagy. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "TDOAuth.h"
#import "LoginViewController.h"

@interface PlmCalls : NSObject

+ (Boolean)storePosition:(NSString*)position
             withName:(NSString*)name
            withImage:(UIImage*)image;
+ (NSDictionary*)getPositions;
+ (UIImage*)getImage:(NSNumber*)itemId;
+ (Boolean)logOut;

@end
