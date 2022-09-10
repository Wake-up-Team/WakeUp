using Godot;

public class ghost : KinematicBody2D
{
    private RayCast2D _rayCastBottomLeft;
    private RayCast2D _rayCastBottomRight;
    private RayCast2D _rayCastMidLeft;
    private RayCast2D _rayCastMidRight;

    private Vector2 _velocity;
    private int _gravity = 200;
    private int _speed = 25;
    private int _health = 3;

    private AnimatedSprite _sprite;

    public override void _Ready()
    {
        _rayCastBottomLeft = GetNode<RayCast2D>("RayCastBottomLeft");
        _rayCastBottomRight = GetNode<RayCast2D>("RayCastBottomRight");
        _rayCastMidLeft = GetNode<RayCast2D>("RayCastMidLeft");
        _rayCastMidRight = GetNode<RayCast2D>("RayCastMidRight");
        _sprite = GetNode<AnimatedSprite>("AnimatedSprite");
        _velocity.x = _speed;

    }

    public override void _Process(float delta)
    {
        _velocity.y += _gravity * delta;
        if (_velocity.y > _gravity)
        {
            _velocity.y = _gravity;
        }

        if (!_rayCastBottomRight.IsColliding() || _rayCastMidRight.IsColliding())
        {
            _velocity.x = -_speed;
            _sprite.FlipH = true;
        }

        if (!_rayCastBottomLeft.IsColliding() || _rayCastMidLeft.IsColliding())
        {
            _velocity.x = _speed;
            _sprite.FlipH = false;
        }
        MoveAndSlide(_velocity, Vector2.Up);
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
    }

    public void _on_Area2D_body_entered(object body)
    {
        if (body is Lightning lightning)
        {
            TakeDamage(1);
            GetNode<AudioStreamPlayer>("AudioStreamPlayer").Play();
            if (_health < 1)
            {
                GetNode<AnimatedSprite>("AnimatedSprite").Play("Death");
                GetNode<CollisionShape2D>("Area2D/CollisionShape2D").SetDeferred("disabled", true);
                _velocity = Vector2.Zero;
            }
            else
            {
                GetNode<AnimatedSprite>("AnimatedSprite").Play("TakeDamage");
                _velocity = MoveAndSlide(new Vector2(0, -50), Vector2.Up);
            }
            lightning.QueueFree();
        }

        if (body is PlayerController playerController)
        {
            playerController.TakeDamage(3);
        }
    }

    private void _on_AudioStreamPlayer_finished()
    {
        if (_health < 1)
        {
            QueueFree();
        }
    }

    private void _on_AnimatedSprite_animation_finished()
    {
        if (GetNode<AnimatedSprite>("AnimatedSprite").Animation == "TakeDamage")
        {
            GetNode<AnimatedSprite>("AnimatedSprite").Play("ghost");
        }
        else if (GetNode<AnimatedSprite>("AnimatedSprite").Animation == "Death")
        {
            GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("disabled", true);
            Hide();
        }
    }
}
