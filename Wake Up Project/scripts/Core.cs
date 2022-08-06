using Godot;

public class Core : Node2D
{

    [Export]
    public Position2D RespPos;

    public override void _Ready()
    {
        GetNode<MarginContainer>("HUD/MarginContainer").Hide();
    }


    public void RespawnPlayer(Vector2 spawnPosition)
    {
        PlayerController playerController = GetNode<PlayerController>("Player");
        playerController.RespawnPlayer(spawnPosition);
    }

    private void _on_Player_Death()
    {
        GetNode<MarginContainer>("HUD/MarginContainer").Show();
        GetNode<Button>("HUD/MarginContainer/VBoxContainer/RestartButton").GrabFocus();
        GetNode<HUD>("HUD").ShowGameOver();
    }

    private void _on_HUD_RestartGame()
    {
        Vector2 spawnPosition = GetNode<Position2D>("RespPos").Position;
        RespawnPlayer(spawnPosition);

        var hud = GetNode<HUD>("HUD");
        hud.ShowMessage("Get Ready!");
    }
}
