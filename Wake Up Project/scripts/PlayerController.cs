using Godot;

public class PlayerController : KinematicBody2D
{
    [Signal]
    public delegate void CoinCollected();

    [Signal]
    public delegate void DamageTaken(int numberOfHeartsToHide);

    private Vector2 velocity = new Vector2();

    private int speed = 100;
    private int gravity = 400;
    private int jumpHeigth = 200;

    private float acceleration = 0.25f;
    private float friction = 0.5f;

    private bool isInAir = false;

    private AnimatedSprite animatedPlayerSprite;

    private int direction = 0;

    private bool isTakingDamage = false;

    private int health = 3;

    [Signal]
    public delegate void Death();

    public override void _Ready()
    {
        animatedPlayerSprite = GetNode<AnimatedSprite>("AnimatedSprite");
    }


    public override void _Process(float delta)
    {

        if (health != 0)
        {
            velocity.y += gravity * delta;
            direction = 0;
            if (!isTakingDamage)
            {
                if (Input.IsActionPressed("move_left"))
                {
                    direction = -1;
                    animatedPlayerSprite.FlipH = true;

                }
                if (Input.IsActionPressed("move_right"))
                {
                    direction = 1;
                    animatedPlayerSprite.FlipH = false;
                }

                if (IsOnFloor())
                {
                    if (Input.IsActionJustPressed("jump"))
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
            velocity = MoveAndSlide(velocity, Vector2.Up);
        }

    }


    public void TakeDamage(int damage)
    {
        EmitSignal("DamageTaken", damage);
        health -= damage;
        GD.Print("health: " + health);
        animatedPlayerSprite.Play("TakeDamage");
        velocity = MoveAndSlide(new Vector2(800f * -direction, -120), Vector2.Up);
        isTakingDamage = true;
        if (health <= 0)
        {
            health = 0;
            animatedPlayerSprite.Play("default death");
            GD.Print("Player dead");
        }
    }

    private void _on_AnimatedSprite_animation_finished()
    {
        if (animatedPlayerSprite.Animation == "default death")
        {
            animatedPlayerSprite.Stop();
            GD.Print("Anim finished");
            Hide();
            EmitSignal(nameof(Death));
        }
    }

    public void RespawnPlayer(Vector2 spawnPosition)
    {
        Position = spawnPosition;
        Show();
        health = 3;
    }
}
