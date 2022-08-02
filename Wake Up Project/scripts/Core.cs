using Godot;
using System;

public class Core : Node2D
{

    [Export]
    public Position2D RespPos;

    
    public override void _Ready()
    {

    }


    public void RespawnPlayer()
    {
        PlayerController playerController = GetNode<PlayerController>("Player");
        playerController.GlobalPosition = RespPos.GlobalPosition;
        playerController.RespawnPlayer();
    }

    private void _on_Player_Death()
    {
        RespawnPlayer();
    }
}
