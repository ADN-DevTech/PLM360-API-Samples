//
//  Msg.m
//  GpsTracker
//
//  Created by Adam Nagy on 03/10/2013.
//  Copyright (c) 2013 Adam Nagy. All rights reserved.
//

#import "Msg.h"

static Msg *instance;
static UIActivityIndicatorView *activity;
static void (^callHandler1)(void);
static void (^callHandler2)(NSString*);

@implementation Msg

+ (void)inform:(NSString*)message
{
  UIAlertView *alert =
  [[UIAlertView alloc]
   initWithTitle:@"Info"
   message:message
   delegate:nil
   cancelButtonTitle:@"OK"
   otherButtonTitles:nil];
  
   [alert show];
}

+ (void)askQuestion:(NSString*)question
option1:(NSString*)option1
option2:(NSString*)option2
handler1:(void (^)(void))handler1
handler2:(void (^)(NSString*))handler2
{
  callHandler1 = handler1;
  callHandler2 = handler2;
  
  if (!instance)
    instance = [Msg alloc];
  
  UIAlertView *alert =
  [[UIAlertView alloc]
   initWithTitle:@"Question"
   message:question
   delegate:instance
   cancelButtonTitle:option1
   otherButtonTitles:option2, nil];
  
   [alert show];
}

+ (void)askInfo:(NSString*)info
option1:(NSString*)option1
option2:(NSString*)option2
handler1:(void (^)(void))handler1
handler2:(void (^)(NSString*))handler2
{
  callHandler1 = handler1;
  callHandler2 = handler2;
  
  if (!instance)
    instance = [Msg alloc];
  
  UIAlertView *alert =
  [[UIAlertView alloc]
   initWithTitle:@"Question"
   message:info
   delegate:instance
   cancelButtonTitle:option1
   otherButtonTitles:option2, nil];
  
   alert.alertViewStyle = UIAlertViewStylePlainTextInput;
  
   [alert show];
}

- (void)alertView:(UIAlertView *)alertView
        didDismissWithButtonIndex:(NSInteger)buttonIndex
{
  // Cancel = 0 / Try again = 1
  if (buttonIndex == 0)
    callHandler1();
  else
  {
    callHandler2([alertView textFieldAtIndex:0].text);
  }
}

@end
