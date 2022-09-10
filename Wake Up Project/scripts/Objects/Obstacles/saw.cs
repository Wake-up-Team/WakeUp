using Godot;

public class saw : Node2D
{
    private void _on_Area2D_body_entered(object body)
    {
        if (body is PlayerController playerController)
        {
            playerController.TakeDamage(1);
        }
    }
}
