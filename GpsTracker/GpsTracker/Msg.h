//
//  Msg.h
//  GpsTracker
//
//  Created by Adam Nagy on 03/10/2013.
//  Copyright (c) 2013 Adam Nagy. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface Msg : NSObject

+ (void)inform:(NSString*)message;

+ (void)askQuestion:(NSString*)question
option1:(NSString*)option1
option2:(NSString*)option2
handler1:(void (^)(void))handler1
handler2:(void (^)(NSString*))handler2;

+ (void)askInfo:(NSString*)info
option1:(NSString*)option1
option2:(NSString*)option2
handler1:(void (^)(void))handler1
handler2:(void (^)(NSString*))handler2;

@end
