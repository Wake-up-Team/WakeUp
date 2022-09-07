using Godot;

public class ElevatorIdling : Node2D
{
    private AnimationPlayer _animationPlayer;
    private AnimationPlayer _musicAnimationPlayer;

    public override void _Ready()
    {
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        _musicAnimationPlayer = GetNode<AnimationPlayer>("MusicAnimationPlayer");
        _animationPlayer.Play("clearUp");
        _musicAnimationPlayer.Play("music");
    }

    private void _on_Timer_timeout()
    {
        _animationPlayer.PlayBackwards("clearUp");
        _musicAnimationPlayer.PlayBackwards("music");
    }
}
