//
//  ImageViewController.m
//  GpsTracker
//
//  Created by Adam Nagy on 01/10/2013.
//  Copyright (c) 2013 Adam Nagy. All rights reserved.
//

#import "ImageViewController.h"
#import "PlmCalls.h"

static NSNumber *itemId;

@interface ImageViewController ()

@end

@implementation ImageViewController

- (id)initWithNibName:(NSString *)nibNameOrNil bundle:(NSBundle *)nibBundleOrNil
{
  self = [super initWithNibName:nibNameOrNil bundle:nibBundleOrNil];
  if (self)
  {
    // Custom initialization
  }
  return self;
}

- (void)viewDidLoad
{
  [super viewDidLoad];
	// Do any additional setup after loading the view.
  
  _activityIndicator.hidden = false;
  dispatch_async
  (
   dispatch_get_global_queue(DISPATCH_QUEUE_PRIORITY_HIGH, 0),
   ^{
     // Fetch the picture from the item
     UIImage *image = [PlmCalls getImage:itemId];
     
     // Now move back to the main thread as we have to
     // update UI stuff
     dispatch_sync
     (dispatch_get_main_queue(),
      ^{
        _imageView.image = image;
        _activityIndicator.hidden = true;
      });
   });
}

- (void)didReceiveMemoryWarning
{
  [super didReceiveMemoryWarning];
  // Dispose of any resources that can be recreated.
}

- (NSUInteger)supportedInterfaceOrientations
{
  return UIInterfaceOrientationMaskAll;
}

// If the user tapped on the screen then let's go back to the original view
- (IBAction)tapGesture:(id)sender
{
  [self dismissViewControllerAnimated:true completion:nil];
}

//////////////////////////////////////////////////////////////////////
// Interface /////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////

+ (void)setItemId:(NSNumber*)ItemId
{
  itemId = ItemId;
}

@end
