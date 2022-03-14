extends KinematicBody2D

export var colors = [Color.royalblue, Color.darkorange, Color.darkgreen, Color.darkmagenta, Color.orange, Color.cyan, Color.red]
export var initial_speed = 250
export var speed = 0
export var can_move = false
export var direction = Vector2(0,0)

##
## Godot Methods
##

func _ready():
	start_moving()

func _process(delta):
	if Input.is_action_just_pressed("play_logo"):
		start_moving()
		
	if Input.is_action_just_pressed("increase_speed"):
		increase_speed()
	
	if Input.is_action_just_pressed("decrease_speed"):
		decrease_speed()
		
	if Input.is_action_just_pressed("reset_speed"):
		reset_speed()
	
	if can_move:
		calc_screen_border_collision()

func _physics_process(delta):
	position += direction.normalized() * speed * delta

##
## Game Logic
##

func start_moving():
	position_logo_at_center()
	change_color()
	speed = initial_speed
	can_move = true
	direction = Vector2(1,1)

func calc_screen_border_collision():
	var screen_size = get_viewport_rect().size 
	var collision_box = get_node("CollisionShape2D").get_shape().extents
	
	var collision_right = position.x + collision_box.x * 2
	var collision_left = position.x - collision_box.x * 2
	var collision_down = position.y + collision_box.y * 2
	var collision_up = position.y - collision_box.y * 2
	
	if collision_right > screen_size.x:
		direction = reflect_x_direction(direction)
		change_color()
	
	if collision_left < 0:
		direction = reflect_x_direction(direction)
		change_color()
	
	if collision_down > screen_size.y:
		direction = reflect_y_direction(direction)
		change_color()
		
	if collision_up < 0:
		direction = reflect_y_direction(direction)
		change_color()

func reflect_x_direction(dir):
	dir.x *= -1
	return dir

func reflect_y_direction(dir):
	dir.y *= -1
	return dir

func change_color():
	var color: Color = colors[randi() % colors.size()]
	get_node("Sprite").modulate = color
	
	randomize()
	colors.shuffle()

func randomize_color():
	var color: Color = colors[randi() % colors.size()]
	randomize()
	colors = colors.shuffle()

func position_logo_at_center():
	var screen_size = get_viewport_rect().size
	position = Vector2(screen_size.x/2, screen_size.y/2)

func increase_speed():
	speed += 100

func decrease_speed():
	speed -= 100

func reset_speed():
	speed = initial_speed
