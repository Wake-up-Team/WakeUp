using Godot;

public class Core : Node2D
{

    [Export]
    public Position2D RespPos;


    public override void _Ready()
    {

    }


    public void RespawnPlayer(Vector2 spawnPosition)
    {
        PlayerController playerController = GetNode<PlayerController>("Player");
        playerController.RespawnPlayer(spawnPosition);
    }

    private void _on_Player_Death()
    {
        Vector2 spawnPosition = GetNode<Position2D>("RespPos").Position;
        RespawnPlayer(spawnPosition);
    }
}
