using Godot;
using System;

public class Fireball : RigidBody2D
{
    private int speed = 150;
    private float lifeSpan = 20;
    public override void _Ready()
    {
        
    }

    public override void _Process(float delta)
    {
        Position += Transform.x * delta * speed;
        lifeSpan -= delta;
        if(lifeSpan < 0)
        {
            QueueFree();
        }
    }

    private void _on_Area2D_body_entered(object body)
    {
        QueueFree();
    }
}
