using Godot;

public class Core : Node2D
{

    [Export]
    public Position2D RespPos;
    private int _score = 0;
    public override void _Ready()
    {
        GetNode<MarginContainer>("HUD/MarginContainer").Hide();
        GetNode<Timer>("ScoreTimer").Start();
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
        GetNode<HUD>("HUD").SetScore(_score);
        GetNode<Timer>("ScoreTimer").Stop();
    }

    private void _on_HUD_RestartGame()
    {
        GetNode<MarginContainer>("HUD/MarginContainer").Hide();
        _score = 0;

        Vector2 spawnPosition = GetNode<Position2D>("RespPos").Position;
        RespawnPlayer(spawnPosition);

        var hud = GetNode<HUD>("HUD");
        GetNode<Timer>("ScoreTimer").Start();
    }

    private void _on_ScoreTimer_timeout()
    {
        _score++;
    }
}
