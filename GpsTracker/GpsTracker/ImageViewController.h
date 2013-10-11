//
//  ImageViewController.h
//  GpsTracker
//
//  Created by Adam Nagy on 01/10/2013.
//  Copyright (c) 2013 Adam Nagy. All rights reserved.
//

#import <UIKit/UIKit.h>

@interface ImageViewController : UIViewController <UIGestureRecognizerDelegate>
- (IBAction)tapGesture:(id)sender;
@property (weak, nonatomic) IBOutlet UIImageView *imageView;
@property (weak, nonatomic) IBOutlet UIActivityIndicatorView *activityIndicator;

+ (void)setItemId:(NSNumber*)ItemId;

@end
