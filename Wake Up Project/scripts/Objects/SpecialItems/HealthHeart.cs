using Godot;
using System;

public class HealthHeart : Area2D
{
    private AnimationPlayer _animationPlayer;

    public override void _Ready()
    {
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        _animationPlayer.Play("Idle");
    }

    private void _on_HealthHeart_body_entered(Node body)
    {
        if (body is PlayerController player && player.CurHealth < player.MaxHealth)
        {
            player.IncreaseHealth();
            GetNode<AudioStreamPlayer2D>("CollectedSound").Play();
            Hide();
            GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("disabled", true);
        }
    }

    private void _on_CollectedSound_finished()
    {
        QueueFree();
    }
}

