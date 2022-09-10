using Godot;

public class PlayerController : KinematicBody2D
{
    [Signal]
    public delegate void CoinCollected();

    [Signal]
    public delegate void DamageTaken(int numberOfHeartsToHide);

    public int NumberOfCollectedCoins { get; set; } = 0;
    public bool HasEnoughCoinsToOpenTheDoor { get; set; } = false;

    private Vector2 _velocity = new Vector2();
    private int _speed = 100;
    private int _gravity = 400;
    private int _jumpHeigth = 200;
    private float _acceleration = 0.25f;
    private float _friction = 0.5f;
    private int _impulse = 5;
    private int _direction = 0;


    private bool _isInAir = false;
    private bool _isTakingDamage = false;
    private int _health;
    public int MaxHealth { get; private set; } = 3;
    public int CurHealth { get => _health; }

    private float _shootTimer = 1f;
    private float _shootTimerReset = 1f;
    private bool _isAbleToShoot = true;
    private bool _directionForFireball = true;
    private Position2D _positionOfGun;

    [Export]
    private PackedScene _fireball;

    [Export]
    private PackedScene _lightning;

    [Export]
    private bool _isFireballAvailable;

    [Export]
    private bool _isLightningAvailable;

    [Signal]
    public delegate void Death();

    private AnimatedSprite _animatedPlayerSprite;

    public override void _Ready()
    {
        _animatedPlayerSprite = GetNode<AnimatedSprite>("AnimatedSprite");
        _positionOfGun = GetNode<Position2D>("GunRight");
        _health = MaxHealth;
    }

    public bool IsAlive()
    {
        return _health > 0;
    }

    public override void _PhysicsProcess(float delta)
    {

        if (_health != 0)
        {
            _velocity.y += _gravity * delta;
            _direction = 0;
            if (Input.IsActionJustPressed("lightning") && _isLightningAvailable)
            {
                if (_isAbleToShoot)
                {
                    GD.Print("fire" + (GetGlobalMousePosition() - this.GlobalPosition));
                    if (new Vector2(GetGlobalMousePosition() - this.GlobalPosition).x < 0)
                    {
                        _positionOfGun = GetNode<Position2D>("GunLeft");
                    }
                    else
                    {
                        _positionOfGun = GetNode<Position2D>("GunRight");
                    }

                    _positionOfGun.LookAt(GetGlobalMousePosition());
                    Lightning lightning = _lightning.Instance() as Lightning;
                    Owner.AddChild(lightning);
                    lightning.GlobalTransform = _positionOfGun.GlobalTransform;
                    _isAbleToShoot = false;
                    _shootTimer = _shootTimerReset;
                }
            }

            if (Input.IsActionJustPressed("fire") && _isFireballAvailable)
            {
                if (_isAbleToShoot)
                {
                    if (new Vector2(GetGlobalMousePosition() - this.GlobalPosition).x < 0)
                    {
                        _positionOfGun = GetNode<Position2D>("GunLeft");
                    }
                    else
                    {
                        _positionOfGun = GetNode<Position2D>("GunRight");
                    }

                    _positionOfGun.LookAt(GetGlobalMousePosition());
                    Fireball fireball = _fireball.Instance() as Fireball;
                    Owner.AddChild(fireball);
                    fireball.GlobalTransform = _positionOfGun.GlobalTransform;
                    _isAbleToShoot = false;
                    _shootTimer = _shootTimerReset;
                }
            }

            if (_shootTimer <= 0)
            {
                _isAbleToShoot = true;
            }
            else
            {
                _shootTimer -= delta;
            }

            if (!_isTakingDamage)
            {
                if (Input.IsActionPressed("move_left"))
                {
                    _directionForFireball = true;
                    _positionOfGun = GetNode<Position2D>("GunLeft");
                    _direction = -1;
                    _animatedPlayerSprite.FlipH = true;
                }

                if (Input.IsActionPressed("move_right"))
                {
                    _directionForFireball = false;
                    _positionOfGun = GetNode<Position2D>("GunRight");
                    _direction = 1;
                    _animatedPlayerSprite.FlipH = false;
                }

                if (IsOnFloor())
                {
                    if (GetNode<RayCast2D>("DownRaycast").IsColliding() && Input.IsActionJustPressed("move_down"))
                    {
                        Position = new Vector2(Position.x, Position.y + 1);
                        GD.Print("Platform detected");
                    }
                    else if (Input.IsActionJustPressed("jump"))
                    {
                        _velocity.y -= _jumpHeigth;
                        _animatedPlayerSprite.Play("Jump");
                        _isInAir = true;
                    }
                    else
                    {
                        _isInAir = false;
                    }
                }
            }

            if (_direction != 0)
            {
                _velocity.x = Mathf.Lerp(_velocity.x, _direction * _speed, _acceleration);
                if (!_isInAir)
                {
                    _animatedPlayerSprite.Play("running");
                }
            }
            else
            {
                _velocity.x = Mathf.Lerp(_velocity.x, 0, _friction);
                if (_velocity.x < 5 && _velocity.x > -5)
                {
                    if (!_isInAir && !_isTakingDamage && IsOnFloor())
                    {
                        _animatedPlayerSprite.Play("idle");
                    }
                    _isTakingDamage = false;
                }
            }

            _velocity = MoveAndSlide(_velocity, Vector2.Up, false, 4, 0.785398f, false);
            for (int i = 0; i < GetSlideCount(); i++)
            {
                var collision = GetSlideCollision(i);
                if (((Node)collision.Collider) is MovableBlock)
                {
                    ((RigidBody2D)collision.Collider).ApplyCentralImpulse(-collision.Normal * _impulse);
                }
            }
        }

    }


    public void TakeDamage(int damage)
    {
        EmitSignal("DamageTaken", damage);
        _health -= damage;
        _animatedPlayerSprite.Play("TakeDamage");
        _velocity = MoveAndSlide(new Vector2(800f * -_direction, -120), Vector2.Up);
        _isTakingDamage = true;
        if (_health <= 0)
        {
            _health = 0;
            _animatedPlayerSprite.Play("default death");
        }

        if (_health < 1)
        {
            GetNode<AudioStreamPlayer>("DeathSound").Play();
            GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("disabled", true);
        }
        else
        {
            GetNode<AudioStreamPlayer>("TakeDamageSound").Play();
        }
    }

    private void _on_AnimatedSprite_animation_finished()
    {
        if (_animatedPlayerSprite.Animation == "default death")
        {
            _animatedPlayerSprite.Stop();
            Hide();
            EmitSignal(nameof(Death));
        }
    }

    private void _on_FallZone_body_entered(object body)
    {
        if (body is PlayerController)
        {
            TakeDamage(3);
        }
    }

    [Signal]
    private delegate void HealthIncreased(int additionalHealthPoints);

    public void IncreaseHealth()
    {
        _health += 1;
        if (_health > MaxHealth)
        {
            _health = MaxHealth;
        }
        EmitSignal(nameof(HealthIncreased), 1);
    }
}
