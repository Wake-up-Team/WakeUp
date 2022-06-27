using Godot;
using System;

public class Spike : Node2D
{
    public override void _Ready()
    {
        
    }

    private void _on_Area2D_body_entered(object body) {
        if (body is PlayerController && body is KinematicBody2D) {
            PlayerController playerController = body as PlayerController;
            playerController.TakeDamage();
        }
    }
}
