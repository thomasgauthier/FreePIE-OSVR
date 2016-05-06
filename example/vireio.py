#requires VireioSMT.dll in "C:\Program Files (x86)\FreePIE\plugins\"
#can be found here https://drive.google.com/file/d/0B4uNjv1ngcxAMHlkTlFIZlVaNGs/view?usp=sharing

global yawModifier
global pitchModifier
global rollModifier

def update():
    global yaw
    yaw = yawModifier*filters.continuousRotation(OSVR.yaw)
    global pitch
    pitch = pitchModifier*filters.continuousRotation(OSVR.pitch)
    global roll
    roll = rollModifier*filters.continuousRotation(OSVR.roll)
    
    global x
    x = OSVR.x
    global y
    y = OSVR.y
    global z
    z = OSVR.z

if starting:
    yawModifier = 1.0
    pitchModifier = 1.0
    rollModifier = 1.0

    centerYaw = 0
    centerPitch = 0
    centerRoll = 0
   	
    centerX = 0
    centerY = 0
    centerZ = 0
    
    yaw = 0
    pitch = 0
    roll = 0
    
    x = 0
    y = 0
    z = 0



vireioSMT.roll  = roll - centerRoll
vireioSMT.yaw = yaw - centerYaw
vireioSMT.pitch = pitch - centerPitch

vireioSMT.x  = x - centerX
vireioSMT.y = y - centerY
vireioSMT.z = z - centerZ


update()

if keyboard.getKeyDown(Key.LeftControl) and keyboard.getPressed(Key.C):
    centerYaw = yaw
    centerPitch = pitch
    centerRoll = roll
    centerX = x
    centerY = y
    centerZ = z
    
if keyboard.getKeyDown(Key.PageDown):
    yawModifier *= -1
    
if keyboard.getKeyDown(Key.PageUp):
    pitchModifier *= -1
    
if keyboard.getKeyDown(Key.Home):
    rollModifier *= -1



diagnostics.watch(vireioSMT.yaw)
diagnostics.watch(vireioSMT.pitch)
diagnostics.watch(vireioSMT.roll)
diagnostics.watch(vireioSMT.z)
diagnostics.watch(vireioSMT.y)
diagnostics.watch(vireioSMT.x)
diagnostics.watch(yawModifier)
diagnostics.watch(pitchModifier)
diagnostics.watch(rollModifier)