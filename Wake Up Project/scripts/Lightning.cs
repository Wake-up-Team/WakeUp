using Godot;
using System;

public class Lightning : RigidBody2D
{
    private int speed = 150;
    private float lifeSpan = 2;
    private AnimatedSprite animatedLightningSprite;

    public override void _Ready()
    {
        animatedLightningSprite = GetNode<AnimatedSprite>("AnimatedLightning");
    }

    public override void _Process(float delta)
    {
        animatedLightningSprite.Play();
        Position += Transform.x * delta * speed;
        lifeSpan -= delta;
        if (lifeSpan < 0)
        {
            QueueFree();
        }
    }

    private bool MustDisappear(Node enteredBody)
    {
        return enteredBody is TileMap || enteredBody is Log || enteredBody is MovableBlock || enteredBody is LogWithoutDamage;
    }

    private void _on_Area2D_body_entered(Node body)
    {
        if (MustDisappear(body) == true)
        {
            QueueFree();
        }
    }
}
