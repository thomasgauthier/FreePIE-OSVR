global head
global yawModifier
global pitchModifier
global rollModifier

def update():
	global head
    global yaw
    yaw = yawModifier*filters.continuousRotation(head.yaw)
    global pitch
    pitch = pitchModifier*filters.continuousRotation(head.pitch)
    global roll
    roll = rollModifier*filters.continuousRotation(head.roll)
	
    global x
    x = head.x
    global y
    y = head.y
    global z
    z = head.z
    

if starting:
	head = OSVR.head()
	
    yawModifier = 1.0
    pitchModifier = -1.0
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

freeTrack.roll  = roll - centerRoll
freeTrack.yaw = yaw - centerYaw
freeTrack.pitch = pitch - centerPitch

freeTrack.x  = x - centerX
freeTrack.y = y - centerY
freeTrack.z = z - centerZ

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

diagnostics.watch(freeTrack.yaw)
diagnostics.watch(freeTrack.pitch)
diagnostics.watch(freeTrack.roll)
diagnostics.watch(freeTrack.x)
diagnostics.watch(freeTrack.y)
diagnostics.watch(freeTrack.z)
diagnostics.watch(yawModifier)
diagnostics.watch(pitchModifier)
diagnostics.watch(rollModifier)