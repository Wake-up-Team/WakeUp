using Godot;

public class Coin : Area2D
{
    private AnimationPlayer _animationPlayer;
    
    public override void _Ready()
    {
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        _animationPlayer.Play("rotation");
    }

    private void _on_body_entered(Node body)
    {
        if (body is PlayerController player)
        {
            GetNode<CollisionShape2D>("CollisionShape2D").QueueFree();
            GetNode<Sprite>("Rotation").Hide();
            GetNode<Sprite>("Collected").Show();
            _animationPlayer.Play("collected");
            player.NumberOfCollectedCoins++;
            player.EmitSignal("CoinCollected");
        }
    }

    private void _on_AnimationPlayer_animation_finished(string animationName)
    {
        if (animationName == "collected")
        {
            QueueFree();
        }
    }
}

