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


freeTrack.pitch  = pitch - centerPitch
freeTrack.yaw = yaw - centerYaw
freeTrack.roll = roll - centerRoll

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
   
diagnostics.watch(freeTrack.yaw)
diagnostics.watch(freeTrack.pitch)
diagnostics.watch(freeTrack.roll)
diagnostics.watch(yawModifier)
diagnostics.watch(pitchModifier)
diagnostics.watch(rollModifier)