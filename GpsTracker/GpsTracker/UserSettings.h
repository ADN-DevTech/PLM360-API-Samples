//
//  UserSettings.h
//  GpsTracker
//
//  Created by Adam Nagy on 04/10/2013.
//  Copyright (c) 2013 Adam Nagy. All rights reserved.
//

#ifndef GpsTracker_UserSettings_h
#define GpsTracker_UserSettings_h

// Fill in these 3 macros with the correct information
#define O2_OAUTH_KEY     @"your.oxygen.key"
#define O2_OAUTH_SECRET  @"your.oxygen.secret.key"
// Do NOT add the "https://" prefix!
#define O2_HOST          @"accounts.autodesk.com"

// Leave these two
#define O2_AUTHORIZE     @"https://" O2_HOST @"/OAuth/Authorize"
#define O2_ALLOW         @"https://" O2_HOST @"/OAuth/Allow"

// Also fill these in
#define PLM_TENANT       @"your.plm.tenant"
#define PLM_TENANT_URL   @"your.tenant.autodeskplm360.net"
#define PLM_WORKSPACE_ID @"workspace.id.storing.the.data"

#endif
