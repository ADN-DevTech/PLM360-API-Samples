Copyright (c) Autodesk, Inc. All rights reserved 

Autodesk PLM360 API Samples
by Adam Nagy - Autodesk Developer Network (ADN)


Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
the Software, and to permit persons to whom the Software is furnished to do so,
subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.


Autodesk PLM360 APISamples
==================

This repository contains 3 samples:

"PLMSampleDesktop"
- A .NET sample application that shows most capabilities of the PLM 360 REST API:
    - Getting the list of workspaces
    - Getting the items in the workspace
    - Getting the data in the items
    - Downloading item file attachments

The .NET sample uses a updated version of RESTSharp by author(source code is not included in this project), find the dll from repository.

"Material Profiler Inventor Add-In + PLM360"
- A .NET Inventor AddIn called Material Profiler, which is storing material related information inside PLM 360 and uses that to provide data about all the materials being used inside the assembly

"GpsTracker"
- An iOS app which let's you take pictures at various locations and uploads that information into a workspace inside PLM 360.
  It also shows on a map all the locations and pictures that are stored inside the workspace. It uses WiFi positioning so that it should work inside a building as well as long as the mobile device is connected to a WiFi network

