using Godot;

public class Lightning : RigidBody2D
{
    private int _speed = 150;
    private float _lifeSpan = 2;
    private AnimatedSprite _animatedLightningSprite;

    public override void _Ready()
    {
        _animatedLightningSprite = GetNode<AnimatedSprite>("AnimatedLightning");
    }

    public override void _Process(float delta)
    {
        _animatedLightningSprite.Play();
        Position += Transform.x * delta * _speed;
        _lifeSpan -= delta;
        if (_lifeSpan < 0)
        {
            QueueFree();
        }
    }

    private bool MustDisappear(Node enteredBody)
    {
        return enteredBody is TileMap || enteredBody is Log || enteredBody is MovableBlock || enteredBody is LogWithoutDamage;
    }

    private void _on_Area2D_body_entered(Node body)
    {
        if (MustDisappear(body) == true)
        {
            QueueFree();
        }
    }
}
