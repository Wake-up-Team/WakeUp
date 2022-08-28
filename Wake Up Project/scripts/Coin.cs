using Godot;

public class Coin : Area2D
{
    private AnimationPlayer _animationPlayer;
    public override void _Ready()
    {
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
    }

    private void _on_body_entered(Node body)
    {
        if (body is PlayerController player)
        {
            _animationPlayer.Play("collected");
            player.EmitSignal("CoinCollected");
            QueueFree();
        }
    }
}

