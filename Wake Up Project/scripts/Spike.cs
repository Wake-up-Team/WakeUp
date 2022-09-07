using Godot;

public class Spike : Node2D
{
    private void _on_Area2D_body_entered(object body)
    {
        if (body is PlayerController playerController && body is KinematicBody2D)
        {
            playerController.TakeDamage(1);
        }
    }
}
