using Godot;
using System;

public class PlayerController : KinematicBody2D
{
    private int speed = 100;
    private int gravity = 1000;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        Vector2 velocity = new Vector2();
        int direction = 0;
        if (Input.IsActionPressed("move_left"))
        {
            velocity.x -= speed;
            direction = -1;
        }
        if (Input.IsActionPressed("move_right"))
        {
            velocity.x += speed;
            direction = 1;
        }
        if (Input.IsActionJustPressed("jump"))
        {
            velocity.y += 100;
        }
        //velocity.y -= gravity;
        MoveAndSlide(velocity, Vector2.Up);
    }
}
