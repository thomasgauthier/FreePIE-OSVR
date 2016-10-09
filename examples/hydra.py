global leftController
global rightController

if starting:
	leftController = OSVR.leftController()
	rightController = OSVR.rightController()
	
	hydra[0].enabled = True
	hydra[0].isDocked = False
	hydra[0].side = 'L'

	hydra[1].enabled = True
	hydra[1].isDocked = False
	hydra[1].side = 'R'

def update():
	global leftController
	global rightController
		
	hydra[0].x = leftController.x * 10000
	hydra[0].y = leftController.y * 10000
	hydra[0].z = leftController.z * 10000
	hydra[0].yaw = leftController.yaw
	hydra[0].pitch = -leftController.pitch
	hydra[0].roll = leftController.roll
	hydra[0].one = leftController.one
	hydra[0].two = leftController.two
	hydra[0].three = leftController.three
	hydra[0].four = leftController.four
	hydra[0].bumper = leftController.bumper
	hydra[0].joybutton = leftController.joystick
	hydra[0].start = leftController.middle
	hydra[0].joyx = leftController.joystickX
	hydra[0].joyy = leftController.joystickY
	hydra[0].trigger = leftController.trigger
	
	hydra[1].x = rightController.x * 10000
	hydra[1].y = rightController.y * 10000
	hydra[1].z = rightController.z * 10000
	hydra[1].yaw = rightController.yaw
	hydra[1].pitch = -rightController.pitch
	hydra[1].roll = rightController.roll
	hydra[1].one = rightController.one
	hydra[1].two = rightController.two
	hydra[1].three = rightController.three
	hydra[1].four = rightController.four
	hydra[1].bumper = rightController.bumper
	hydra[1].joybutton = rightController.joystick
	hydra[1].start = rightController.middle
	hydra[1].joyx = rightController.joystickX
	hydra[1].joyy = rightController.joystickY
	hydra[1].trigger = rightController.trigger
	
update()
	
diagnostics.watch(bool(leftController.one))
diagnostics.watch(float(rightController.trigger))
diagnostics.watch(leftController.position.x)
diagnostics.watch(leftController.position.y)
diagnostics.watch(leftController.position.z)
diagnostics.watch(leftController.roll)
diagnostics.watch(leftController.pitch)
diagnostics.watch(leftController.yaw)