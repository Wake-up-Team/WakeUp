using Godot;

public class Log : StaticBody2D
{
    private AnimationPlayer _animationPlayer;

    public override void _Ready()
    {
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
    }

    private void _on_Log_body_entered(Node body)
    {
        if (body is PlayerController || body is Fireball)
        {
            _animationPlayer.Play("burning");
        }
    }

    private void _on_AnimationPlayer_animation_finished(string animationName)
    {
        QueueFree();
    }
}
