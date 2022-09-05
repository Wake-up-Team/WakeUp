using Godot;
using System;

public class Fireball : RigidBody2D
{
    private int speed = 150;
    private float lifeSpan = 2;
    private AnimatedSprite animatedFireballSprite;
   
    public override void _Ready()
    {
        //EmitSignal("FireballAppeared");
        animatedFireballSprite = GetNode<AnimatedSprite>("AnimatedFireball");
        //EmitSignal("FireballAppeared");
    }

    public override void _Process(float delta)
    {
        
        animatedFireballSprite.Play();
        
                
        Position += Transform.x * delta * speed;
        lifeSpan -= delta;
        if(lifeSpan < 0)
        {
            QueueFree();
        }
    }
    private void _on_Area2D_body_entered(Node body)
    {
        if(body is TileMap)
         
            QueueFree();
    }
}
