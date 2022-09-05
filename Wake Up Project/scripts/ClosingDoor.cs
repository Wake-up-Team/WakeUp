using Godot;

public class ClosingDoor : Area2D
{
    private AnimationPlayer _animationPlayer;
    private bool _theDoorIsAlreadyClosed = false;
    public override void _Ready()
    {
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
    }

    private void _on_ClosingDoor_body_exited(Node body)
    {
        if (_theDoorIsAlreadyClosed == false && body is PlayerController)
        {
            _animationPlayer.Play("close");
        }
    }

    private void _on_AnimationPlayer_animation_finished(string animationName)
    {
        if (animationName == "close")
        {
            _theDoorIsAlreadyClosed = true;
        }
    }
}
