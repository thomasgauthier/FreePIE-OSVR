#requires VireioSMT.dll in "C:\Program Files (x86)\FreePIE\plugins\"
#can be found here https://drive.google.com/file/d/0B5t5hd_Bv-frbWVEcmpnRXZOUFk/edit

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
    

if starting:
    yawModifier = 1.0
    pitchModifier = 1.0
    rollModifier = 1.0

    centerYaw = 0
    centerPitch = 0
    centerRoll = 0
   
    yaw = 0
    pitch = 0
    roll = 0


vireioSMT.roll  = roll - centerRoll
vireioSMT.yaw = yaw - centerYaw
vireioSMT.pitch = pitch - centerPitch

update()

if keyboard.getKeyDown(Key.LeftControl) and keyboard.getPressed(Key.C):
    centerYaw = yaw
    centerPitch = pitch
    centerRoll = roll
    
if keyboard.getKeyDown(Key.PageDown):
    yawModifier *= -1
    
if keyboard.getKeyDown(Key.PageUp):
    pitchModifier *= -1
    
if keyboard.getKeyDown(Key.Home):
    rollModifier *= -1



diagnostics.watch(vireioSMT.yaw)
diagnostics.watch(vireioSMT.pitch)
diagnostics.watch(vireioSMT.roll)
diagnostics.watch(yawModifier)
diagnostics.watch(pitchModifier)
diagnostics.watch(rollModifier)