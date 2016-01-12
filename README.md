#FreePIE-OSVR
OSVR plugin for FreePIE

##Build instructions

This is a .NET 4.5 project created with Visual Studio 2015, it should compile easily as all dependencies are in the root folder of the project. It was compiled using OSVR-Core-Snapshot-v0.6-743-g790672e binaries.

##Using

There is a script example for FreePIE in the example folder. Basically you can access OSVR.yaw, OSVR.pitch and OSVR.roll inside FreePIE. All axes are in radian. osvr_server has to be running.
Positional values (x, y, z) might be added in the future.

##Quirks

Sometime the plugin will output 0 for all values (use *diagnostics.watch(OSVR.yaw)* to test).
The fix I've found for this is to unplug and replug the HDK and then restart osvr_server.