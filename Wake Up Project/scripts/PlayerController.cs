using Godot;

public class PlayerController : KinematicBody2D
{
    [Signal]
    public delegate void CoinCollected();

    [Signal]
    public delegate void DamageTaken(int numberOfHeartsToHide);

    private Vector2 velocity = new Vector2();

    public int NumberOfCollectedCoins { get; set; } = 0;
    public bool HasEnoughCoinsToOpenTheDoor { get; set; } = false;

    private int speed = 100;
    private int gravity = 400;
    private int jumpHeigth = 200;

    private float acceleration = 0.25f;
    private float friction = 0.5f;

    private int impulse = 5;

    private bool isInAir = false;

    private AnimatedSprite animatedPlayerSprite;

    private int direction = 0;

    private bool isTakingDamage = false;

    private int health = 3;

    private float shootTimer = 1f;

    private float shootTimerReset = 1f;


    private bool isAbleToShoot = true;
    private bool directionForFireball = true;
    private Position2D positionOfGun;
    [Export]
    private PackedScene Fireball;
    [Export]
    private PackedScene Lightning;
    [Export]
    private bool isFireballAvailable;
    [Export]
    private bool isLightningAvailable;

    [Signal]
    public delegate void Death();

    public override void _Ready()
    {
        animatedPlayerSprite = GetNode<AnimatedSprite>("AnimatedSprite");
        positionOfGun = GetNode<Position2D>("GunRight");
    }

    public bool IsAlive()
    {
        return health > 0;
    }

    public override void _PhysicsProcess(float delta)
    {

        if (health != 0)
        {
            velocity.y += gravity * delta;
            direction = 0;
            if (Input.IsActionJustPressed("lightning") && isLightningAvailable)
            {
                if (isAbleToShoot)
                {
                    GD.Print("fire" + (GetGlobalMousePosition() - this.GlobalPosition));
                    if (new Vector2(GetGlobalMousePosition() - this.GlobalPosition).x < 0)
                        positionOfGun = GetNode<Position2D>("GunLeft");
                    else
                        positionOfGun = GetNode<Position2D>("GunRight");

                    positionOfGun.LookAt(GetGlobalMousePosition());
                    Lightning lightning = Lightning.Instance() as Lightning;
                    Owner.AddChild(lightning);
                    lightning.GlobalTransform = positionOfGun.GlobalTransform;
                    isAbleToShoot = false;
                    shootTimer = shootTimerReset;
                }
            }
            if (Input.IsActionJustPressed("fire") && isFireballAvailable)
            {
                if (isAbleToShoot)
                {
                    if (new Vector2(GetGlobalMousePosition() - this.GlobalPosition).x < 0)
                    {
                        positionOfGun = GetNode<Position2D>("GunLeft");
                    }
                    else
                    {
                        positionOfGun = GetNode<Position2D>("GunRight");
                    }

                    positionOfGun.LookAt(GetGlobalMousePosition());
                    Fireball fireball = Fireball.Instance() as Fireball;
                    Owner.AddChild(fireball);
                    fireball.GlobalTransform = positionOfGun.GlobalTransform;
                    isAbleToShoot = false;
                    shootTimer = shootTimerReset;
                }
            }

            if (shootTimer <= 0)
            {
                isAbleToShoot = true;
            }
            else
            {
                shootTimer -= delta;
            }

            if (!isTakingDamage)
            {
                if (Input.IsActionPressed("move_left"))
                {
                    directionForFireball = true;
                    positionOfGun = GetNode<Position2D>("GunLeft");
                    direction = -1;
                    animatedPlayerSprite.FlipH = true;
                }
                if (Input.IsActionPressed("move_right"))
                {
                    directionForFireball = false;
                    positionOfGun = GetNode<Position2D>("GunRight");
                    direction = 1;
                    animatedPlayerSprite.FlipH = false;
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
                        velocity.y -= jumpHeigth;
                        animatedPlayerSprite.Play("Jump");
                        isInAir = true;
                    }
                    else
                    {
                        isInAir = false;
                    }
                }
            }



            if (direction != 0)
            {
                velocity.x = Mathf.Lerp(velocity.x, direction * speed, acceleration);
                if (!isInAir)
                {
                    animatedPlayerSprite.Play("running");
                }
            }
            else
            {
                velocity.x = Mathf.Lerp(velocity.x, 0, friction);

                if (velocity.x < 5 && velocity.x > -5)
                {
                    if (!isInAir && !isTakingDamage && IsOnFloor())
                    {
                        animatedPlayerSprite.Play("idle");
                    }
                    isTakingDamage = false;
                }
            }
            velocity = MoveAndSlide(velocity, Vector2.Up, false, 4, 0.785398f, false);
            for (int i = 0; i < GetSlideCount(); i++)
            {
                var collision = GetSlideCollision(i);
                if (((Node)collision.Collider) is MovableBlock)
                {
                    ((RigidBody2D)collision.Collider).ApplyCentralImpulse(-collision.Normal * impulse);
                }
            }
        }

    }


    public void TakeDamage(int damage)
    {
        EmitSignal("DamageTaken", damage);
        health -= damage;
        animatedPlayerSprite.Play("TakeDamage");
        velocity = MoveAndSlide(new Vector2(800f * -direction, -120), Vector2.Up);
        isTakingDamage = true;
        if (health <= 0)
        {
            health = 0;
            animatedPlayerSprite.Play("default death");
        }

        if (health < 1)
        {
            GetNode<AudioStreamPlayer>("DeathSound").Play();
        }
        else
        {
            GetNode<AudioStreamPlayer>("TakeDamageSound").Play();
        }
    }

    private void _on_AnimatedSprite_animation_finished()
    {
        if (animatedPlayerSprite.Animation == "default death")
        {
            animatedPlayerSprite.Stop();
            Hide();
            EmitSignal(nameof(Death));
            CollisionShape2D shape = GetNode<CollisionShape2D>("CollisionShape2D");
            shape.QueueFree();
        }
    }

    private void _on_FallZone_body_entered(object body)
    {

        if (body is PlayerController && body is KinematicBody2D)
        {
            TakeDamage(3);
        }
    }
}
