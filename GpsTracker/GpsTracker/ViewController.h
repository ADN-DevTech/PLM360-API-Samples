//
//  ViewController.h
//  GpsTracker
//
//  Created by Adam Nagy on 30/09/2013.
//  Copyright (c) 2013 Adam Nagy. All rights reserved.
//

#import <UIKit/UIKit.h>
#import <MapKit/MapKit.h>
#import <CoreLocation/CoreLocation.h>
#include "LoginViewController.h"
#include "PlmCalls.h"

@interface ViewController : UIViewController
  <CLLocationManagerDelegate,
  UINavigationControllerDelegate,
  UIImagePickerControllerDelegate,
  UIPopoverControllerDelegate,
  MKMapViewDelegate>

@property (nonatomic, retain) CLLocationManager *locationManager;
@property (weak, nonatomic) IBOutlet MKMapView *mapView;
@property (weak, nonatomic) IBOutlet UIBarButtonItem *refreshButton;
@property (weak, nonatomic) IBOutlet UIBarButtonItem *storeButton;
@property (weak, nonatomic) IBOutlet UIBarButtonItem *showButton;
@property (weak, nonatomic) IBOutlet UIBarButtonItem *loginButton;
@property (weak, nonatomic) IBOutlet UIActivityIndicatorView *activityIndicator;
@property (weak, nonatomic) IBOutlet UIBarButtonItem *tenantButton;

- (IBAction)tenantClick:(id)sender;
- (IBAction)refreshClick:(id)sender;
- (IBAction)storeClick:(id)sender;
- (IBAction)showClick:(id)sender;
- (IBAction)loginClick:(id)sender;

@end
