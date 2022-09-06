using Godot;
using System;

public class ghost : KinematicBody2D
{

    RayCast2D RayCastBottomLeft;

    RayCast2D RayCastBottomRight;

    RayCast2D RayCastMidLeft;

    RayCast2D RayCastMidRight;

    private Vector2 velocity;

    private int gravity = 200;

    private int speed = 25;
    private int health = 3;

    private AnimatedSprite sprite;

    public override void _Ready()
    {
        RayCastBottomLeft = GetNode<RayCast2D>("RayCastBottomLeft");
        RayCastBottomRight = GetNode<RayCast2D>("RayCastBottomRight");
        RayCastMidLeft = GetNode<RayCast2D>("RayCastMidLeft");
        RayCastMidRight = GetNode<RayCast2D>("RayCastMidRight");
        sprite = GetNode<AnimatedSprite>("AnimatedSprite");
        velocity.x = speed;

    }

    public override void _Process(float delta)
    {
        velocity.y += gravity * delta;
        if (velocity.y > gravity)
        {
            velocity.y = gravity;
        }

        if (!RayCastBottomRight.IsColliding() || RayCastMidRight.IsColliding())
        {
            velocity.x = -speed;
            sprite.FlipH = true;
        }
        if (!RayCastBottomLeft.IsColliding() || RayCastMidLeft.IsColliding())
        {
            velocity.x = speed;
            sprite.FlipH = false;
        }
        MoveAndSlide(velocity, Vector2.Up);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 1)
        {
            QueueFree();
        }
    }

    public void _on_Area2D_body_entered(object body)
    {
        if (body is Lightning lightning)
        {
            GetNode<AudioStreamPlayer>("AudioStreamPlayer").Play();
            velocity = MoveAndSlide(new Vector2(0, -50), Vector2.Up);
            TakeDamage(1);
            lightning.QueueFree();
        }
        if (body is PlayerController playerController && body is KinematicBody2D)
        {
            playerController.TakeDamage(3);
        }
    }
}
