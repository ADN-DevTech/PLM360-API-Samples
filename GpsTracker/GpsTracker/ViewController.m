//
//  ViewController.m
//  GpsTracker
//
//  Created by Adam Nagy on 30/09/2013.
//  Copyright (c) 2013 Adam Nagy. All rights reserved.
//

#import "ViewController.h"
#import "ImageViewController.h"
#import <MobileCoreServices/MobileCoreServices.h>
#import "Msg.h"

// We need to create our own annotation object ///////////////////////
@interface MyAnnotation : NSObject <MKAnnotation>

@property (nonatomic) CLLocationCoordinate2D coordinate;

// Title and subtitle for use by selection UI.
@property (nonatomic, readonly, copy) NSString *title;
@property (nonatomic, readonly, copy) NSString *subtitle;

// itemId corresponds to the one in PLM
@property (nonatomic, readonly, copy) NSNumber *itemId;

- (id)initWithCoordinate:(CLLocationCoordinate2D)coord
                withTitle:(NSString*)annoTitle
                withSubtitle:(NSString*)annoSubtitle
                withItemId:(NSString*)annoItemId;
@end

@implementation MyAnnotation

- (id)initWithCoordinate:(CLLocationCoordinate2D)coord
                withTitle:(NSString*)annoTitle
                withSubtitle:(NSString*)annoSubtitle
                withItemId:(NSNumber*)annoItemId
{
  _coordinate = coord;
  _title = annoTitle;
  _subtitle = annoSubtitle;
  _itemId = annoItemId;
  
  return self;
}
@end

// Now the view controller ///////////////////////////////////////////
@interface ViewController ()
@end

@implementation ViewController

- (void)getGpsLocation
{
  _locationManager = [[CLLocationManager alloc] init];
  _locationManager.delegate = self;
  
  _locationManager.distanceFilter =
    kCLDistanceFilterNone;
  _locationManager.desiredAccuracy =
    kCLLocationAccuracyHundredMeters; // 100 m
  
#ifdef DEBUG_STRINGS
  NSLog(@"User latitude: %f",_locationManager.location.coordinate.latitude);
  NSLog(@"User longitude: %f",_locationManager.location.coordinate.longitude);
#endif
  
  // This returns immediately and then "didUpdateLocations"
  // will be called
  [_locationManager startUpdatingLocation];
}

// If the Authorization succeeded and we got the location then this is called
- (void)locationManager:(CLLocationManager *)manager
     didUpdateLocations:(NSArray *)locations __OSX_AVAILABLE_STARTING(__MAC_NA,__IPHONE_6_0)
{
  CLLocation *location = [locations lastObject];
  
#ifdef DEBUG_STRINGS
  NSLog(@"User latitude: %f", location.coordinate.latitude);
  NSLog(@"User longitude: %f", location.coordinate.longitude);
#endif
}

// If the Authorization failed, i.e. user clicked "Don't allow" then
// we can find that out here
- (void)locationManager:(CLLocationManager *)manager didChangeAuthorizationStatus:(CLAuthorizationStatus)status
{
  _refreshButton.enabled = (status == kCLAuthorizationStatusAuthorized);
}

- (void)viewDidLoad
{
  [super viewDidLoad];
	// Do any additional setup after loading the view, typically from a nib.
  
  // To make sure that the cookies sent by PLM 360 are accepted
  [[NSHTTPCookieStorage sharedHTTPCookieStorage]
  setCookieAcceptPolicy:NSHTTPCookieAcceptPolicyAlways];
  
  // Get GPS location
  [self getGpsLocation];
}

- (void)didReceiveMemoryWarning
{
  [super didReceiveMemoryWarning];
  // Dispose of any resources that can be recreated.
}

- (void)removeAnnotationsIncludingUserLocation:(Boolean)removeUserLocation
{
  // Copy your annotations to an array
  NSMutableArray *annotationsToRemove =
  [[NSMutableArray alloc]
   initWithArray: _mapView.annotations];
  
  // Remove the object user location from the list of
  // annotations to remove
  if (!removeUserLocation)
  {
    [annotationsToRemove removeObject: _mapView.userLocation];
    _mapView.showsUserLocation = true;
  }
  else
  {
    _mapView.showsUserLocation = false;
  }
  
  // Remove all annotations in the array from the mapView
  [_mapView removeAnnotations: annotationsToRemove];
}

// Refresh the current position
- (IBAction)tenantClick:(id)sender
{
  [Msg
    askInfo:@"Tenant name / WorkspaceId: \ne.g. mytenant/54"
    option1:@"Cancel"
    option2:@"Done"
    handler1: ^{}
    handler2:
    ^(NSString * name){
      NSArray * info = [name componentsSeparatedByString:@"/"];
      if ([info count] < 2)
      {
        [Msg inform:@"Tenant name and WorkspaceId need to be separated by '/'"];
      }
      else
      {
        self.tenantButton.title = name;
        [PlmCalls setPlmTenantName:[info objectAtIndex:0]];
        [PlmCalls setWsId:[info objectAtIndex:1]];
      }
    }];
}

- (IBAction)refreshClick:(id)sender
{
  [self removeAnnotationsIncludingUserLocation:false];
  
  MKMapRect r = [_mapView visibleMapRect];
  MKMapPoint pt = MKMapPointForCoordinate(
    _locationManager.location.coordinate);
  r.origin.x = pt.x - r.size.width * 0.5;
  r.origin.y = pt.y - r.size.height * 0.5;
  
  [_mapView setVisibleMapRect:r animated:YES];
}

// Store the current position of the user in the database
- (IBAction)storeClick:(id)sender
{
  // Check that there is a location
  if ([CLLocationManager authorizationStatus] !=
    kCLAuthorizationStatusAuthorized)
  {
    [Msg inform:
      @"Location Services not enabled for this app. "
      "You can enable it under "
      "Settings >> Privacy >> Location Services"];
    
    return;
  }

  NSLog(@"startCamera");
  
  if ([UIImagePickerController isSourceTypeAvailable:
       UIImagePickerControllerSourceTypeCamera])
  {
    UIImagePickerController *imagePicker =
    [[UIImagePickerController alloc] init];
    imagePicker.delegate = self;
    imagePicker.sourceType =
    UIImagePickerControllerSourceTypeCamera;
    imagePicker.mediaTypes =
    [NSArray
     arrayWithObjects:
     (NSString*)kUTTypeImage,
     nil];
    imagePicker.allowsEditing = YES;
    
    [self presentViewController:imagePicker
                       animated:YES completion:nil];
  }
}

- (void)imagePickerController:(UIImagePickerController*)picker
didFinishPickingMediaWithInfo:(NSDictionary*)info
{
  [picker dismissViewControllerAnimated:YES completion:nil];
  
  UIImage *image =
  [info objectForKey:UIImagePickerControllerEditedImage];
  
  [self storePositionWithImage:image];
}

- (void)imagePickerControllerDidCancel:(UIImagePickerController*)picker
{
  [picker dismissViewControllerAnimated:YES completion:nil];

  [self storePositionWithImage:nil];  
}

- (void)storePositionWithImage:(UIImage *)image
{
  // Now send it to PLM
  
  CLLocationCoordinate2D coord =
  _locationManager.location.coordinate;
  
  NSString *pos = [NSString stringWithFormat:@"%f+%f",
                   coord.latitude, coord.longitude];
  NSString *name = _loginButton.title;
  
  // Do this in the background
  dispatch_async
  (
   dispatch_get_global_queue(DISPATCH_QUEUE_PRIORITY_HIGH, 0),
   ^{
     Boolean res = false;
     
     @try
     {
     res = [PlmCalls storePosition:pos withName:name withImage:image];
      }
    @catch (NSException *exception)
    {
    }
     
     // Move it back to the main thread for UI
     dispatch_sync
     (dispatch_get_main_queue(),
      ^{
        if (res)
          [Msg inform:@"Upload sucessful!"];
        else
          [Msg inform:@"Could not upload position!"];
      });
   });
}

// Show in the map the currently stored positions
- (IBAction)showClick:(id)sender
{
  [self removeAnnotationsIncludingUserLocation:true];
  
  _activityIndicator.hidden = false;
  _showButton.enabled = false;
  
  // Move this part to the background as it might take some time
  dispatch_async
  (
   dispatch_get_global_queue(DISPATCH_QUEUE_PRIORITY_HIGH, 0),
   ^{
     // Get the positions stored in PLM

     NSDictionary *positions;
     @try
     {
       positions = [PlmCalls getPositions];
     }
     @catch (NSException *exception)
     {
     }
     
     // Now move back to the main thread as we have to
     // update UI stuff
     dispatch_sync
     (dispatch_get_main_queue(),
      ^{  
        if ([positions count] > 0)
          [Msg inform:@"Download sucessful!"];
        else
          [Msg inform:@"Could not download positions!"];
        
        for (NSString *itemId in positions)
        {
          CLLocationCoordinate2D coordinate;
          NSArray *data = [positions objectForKey:itemId];
          NSString *pos = [data objectAtIndex:0];
          NSString *name = [data objectAtIndex:1];
          NSString *date = [data objectAtIndex:2];
          NSArray *values = [pos componentsSeparatedByString:@"+"];
          coordinate.latitude = [[values objectAtIndex:0] doubleValue];
          coordinate.longitude = [[values objectAtIndex:1] doubleValue];
          MyAnnotation *placemark =
          [[MyAnnotation alloc]
           initWithCoordinate:coordinate
           withTitle:name
           withSubtitle:date
           withItemId:itemId];
          
          [_mapView addAnnotation:placemark];
        }
        
        _activityIndicator.hidden = true;
        _showButton.enabled = true;
      });
   });
}

- (MKAnnotationView *)mapView:(MKMapView *)mapView viewForAnnotation:(id <MKAnnotation>)annotation
{
  MKPinAnnotationView *annotationView =
  [[MKPinAnnotationView alloc]
   initWithAnnotation:annotation
   reuseIdentifier:@"loc"];
  
  // The current location pin will look purple
  annotationView.canShowCallout = YES;
  annotationView.pinColor = MKPinAnnotationColorPurple;
  
  // The pins coming from PLM will be red
  if ([annotation isKindOfClass:[MyAnnotation class]])
  {
    annotationView.pinColor = MKPinAnnotationColorRed;
    annotationView.rightCalloutAccessoryView =
    [UIButton buttonWithType:UIButtonTypeDetailDisclosure];
  }
  
  return annotationView;
}

- (void)mapView:(MKMapView *)mapView
 annotationView:(MKAnnotationView *)view
calloutAccessoryControlTapped:(UIControl *)control
{
  MyAnnotation *anno = [view annotation];
  [ImageViewController setItemId:anno.itemId];
  [self performSegueWithIdentifier:@"showImage" sender:view];
}

// Change name of the user
- (IBAction)loginClick:(id)sender
{
  // If we are not logged in then this call will do that
  // and if succeeded then will call the anonymous function
  // passed in to it
  if ([LoginViewController loggedIn])
  {
    [Msg
    askQuestion:@"Log out?"
    option1:@"No"
    option2:@"Yes"
    handler1:^{}
    handler2:
    ^(NSString*name){
      // Log out from PLM if needed
      [PlmCalls logOut];
    
      [LoginViewController logOut:
      ^{
        _loginButton.title = @"Log In";
        _showButton.enabled = false;
        _storeButton.enabled = false;
        [Msg inform:@"Log out successful!"];
      }];
    }];
  }
  else
  {
  [LoginViewController logInIfNeeded:
   ^{
     _loginButton.title = [LoginViewController getUserId];
     _showButton.enabled = true;
     _storeButton.enabled = true;
     
   } currentViewController:self];
  }
  return;
}

@end
