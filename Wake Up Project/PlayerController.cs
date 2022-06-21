using Godot;
using System;

public class PlayerController : KinematicBody2D
{
	private Vector2 velocity = new Vector2();

	private int speed = 100;
	private int gravity = 400;
	private int jumpHeigth = 200;

	private float acceleration = .25f;
	private float friction = .5f;

	private bool isInAir = false;
	public override void _Ready()
	{
		
	}

 public override void _Process(float delta)
 {
	velocity.y += gravity*delta;
	int direction = 0;
	if(Input.IsActionPressed("move_left")) {
		direction = -1;
		GetNode<AnimatedSprite>("AnimatedSprite").FlipH = true;

	}		
	if(Input.IsActionPressed("move_right")) {
		direction = 1;
		GetNode<AnimatedSprite>("AnimatedSprite").FlipH = false;
	}

	if(IsOnFloor()) {
		if (Input.IsActionJustPressed("jump")) {
			velocity.y -= jumpHeigth;
			GetNode<AnimatedSprite>("AnimatedSprite").Play("Jump");
			isInAir = true;
		} 
		else {
			isInAir = false;
		}
	} 

	if(direction != 0) {
		velocity.x = Mathf.Lerp(velocity.x, direction*speed, acceleration);
		if (!isInAir) {
			GetNode<AnimatedSprite>("AnimatedSprite").Play("running");
		}
	} else {
		velocity.x = Mathf.Lerp(velocity.x, 0, friction);
		if (!isInAir) {
			GetNode<AnimatedSprite>("AnimatedSprite").Play("idle");
		}

	}
	velocity = MoveAndSlide(velocity, Vector2.Up);
 }
}
