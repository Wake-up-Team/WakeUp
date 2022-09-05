using Godot;

public class ClosingDoor : Area2D
{
    private AnimationPlayer _animationPlayer;
    public override void _Ready()
    {
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
    }

    private void _on_ClosingDoor_body_exited(Node body)
    {
        if (body is PlayerController)
        {
            _animationPlayer.Play("close");
        }
    }
}
