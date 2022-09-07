using Godot;

public class LogWithoutDamage : StaticBody2D
{
    private AnimationPlayer _animationPlayer;

    public override void _Ready()
    {
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
    }

    private void _on_Area2D_body_entered(Node body)
    {
        if (body is Fireball)
        {
            _animationPlayer.Play("burning");
        }
    }

    private void _on_AnimationPlayer_animation_finished(string animationName)
    {
        QueueFree();
    }
}
