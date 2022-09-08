using Godot;

public class Fireball : RigidBody2D
{
    private int _speed = 150;
    private float _lifeSpan = 2;
    private AnimatedSprite _animatedFireballSprite;

    public override void _Ready()
    {
        _animatedFireballSprite = GetNode<AnimatedSprite>("AnimatedFireball");
    }

    public override void _Process(float delta)
    {
        _animatedFireballSprite.Play();
        Position += Transform.x * delta * _speed;
        _lifeSpan -= delta;
        if (_lifeSpan < 0)
        {
            QueueFree();
        }
    }

    private bool MustDisappear(Node enteredBody)
    {
        return enteredBody is TileMap || enteredBody is Log || enteredBody is LogWithoutDamage || enteredBody is MovableBlock;
    }

    private void _on_Area2D_body_entered(Node body)
    {
        if (MustDisappear(body) == true)
        {
            QueueFree();
        }
    }
}
