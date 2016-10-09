#FreePIE-OSVR
OSVR plugin for FreePIE

##Build instructions

This is a .NET 4.5 project created with Visual Studio 2015. You need FreePIE and OSVR core installed; if they are installed somewhere other than the default you need to set their paths in Project > Properties > Reference Paths.

##Using

Script examples can be found in the examples directory. osvr_server has to be running.

The plugin provides a global variable called `OSVR`. You can access OSVR interfaces by calling these methods on the `OSVR` object:

### Analog interface

    OSVR.analog(String path) 

Returns an Analog interface. You can use this object like a float (or double). Sometimes you may need to explicitly cast to float, or you can also use the value property. Eg:

    joystickX = OSVR.analog("/controller/left/joystick/x")
    hydra[0].joyx = joystickX
    diagnostics.watch(float(joystickX))
    diagnostics.watch(joystickX.value)

### Button interface

	OSVR.button(String path)

Returns a Button interface. You can use this object like a boolean value. Eg:

    bumper = OSVR.Button("/controller/left/bumper")
    hydra[0].bumper = bumper
    diagnostics.watch(bool(bumper))
    diagnostics.watch(bumper.value)

### Direction interface

    OSVR.direction(String path)

Returns a Direction interface. Like a three dimensional vector with properties `(double) x`, `y` and `z`.

### EyeTracker interface

    OSVR.eyeTracker(String path)

Returns an EyeTracker interface. Properties are `(bool) blink`, `(Vec2) direction2D`, `(Vec3) direction3D`, `(Vec3) basePoint3D`, `(bool) direction3DValid`, `(bool) basePoint3DValid`

### Location2D interface

    OSVR.location2D(String path)

Returns a Location interface. Properties `(double) x` and `y`

### Locomotion interface

    OSVR.locomotion(String path)

Returns a Locomotion interface. Properties are `(Vec2) position` and `(Vec2) velocity`

### Tracker interface

    OSVR.tracker(String path)

Returns a Tracker interface. Properties are `(Vec3) position`, `(Quaternion) orientation`, `(double) x`, `y`, `z`, `(double) roll`, `pitch`, `yaw`. All axes are in radian. 

### Convenience functions

    OSVR.head()
	
Returns a Tracker interface for "/me/head"

    OSVR.leftHand()
    OSVR.rightHand()

Return Tracker interfaces for "/me/hands/left" and "/me/hands/right"

    OSVR.leftController()
	OSVR.rightController()

Return Controller interfaces for left and right controllers. In addition to behaving like Tracker interfaces, these have properties:

    Button one
    Button two
    Button three
    Button four
    Button bumper
    Button joystick
    Button middle
    Analog joystickX
    Analog joystickY
    Analog trigger