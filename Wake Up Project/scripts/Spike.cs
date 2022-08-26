using Godot;

public class Spike : Node2D
{
    public override void _Ready()
    {

    }

    private void _on_Area2D_body_entered(object body)
    {
        if (body is PlayerController playerController && body is KinematicBody2D)
        {
            playerController.TakeDamage(1);
        }
    }
}
