using Godot;

public class Core : Node2D
{

    [Export]
    public Position2D RespPos;
    private int _score = 0;

    public Vector2 ScreenSize; // Size of the game window.

    private void HideAllHudContent()
    {
        GetNode<HUD>("HUD").HideAllContent();
    }

    public override void _Ready()
    {
        ScreenSize = GetViewportRect().Size;
        HideAllHudContent();
    }

    private void ShowPosthumousMenu()
    {
        GetNode<MarginContainer>("HUD/MarginContainer/VBoxContainer/Score").Show();
        GetNode<Button>("HUD/MarginContainer/VBoxContainer/RestartButton").Show();
        GetNode<Button>("HUD/MarginContainer/VBoxContainer/BackToMenuButton").Show();
        GetNode<Button>("HUD/MarginContainer/VBoxContainer/RestartButton").GrabFocus();
    }

    private void _on_Player_Death()
    {
        ShowPosthumousMenu();
        GetNode<HUD>("HUD").ShowGameOver();
        GetNode<HUD>("HUD").SetScore(_score);
    }

    private void _on_HUD_RestartGame()
    {
        GetNode<SceneSwitcher>("/root/SceneSwitcher").SwitchScene("res://scenes/Core.tscn");
    }

    private void ShowPauseMenu()
    {
        GetNode<Button>("HUD/MarginContainer/VBoxContainer/RestartButton").Show();
        GetNode<Button>("HUD/MarginContainer/VBoxContainer/ResumeButton").Show();
        GetNode<Button>("HUD/MarginContainer/VBoxContainer/PauseMenuButton").Show();
        GetNode<Button>("HUD/MarginContainer/VBoxContainer/ResumeButton").GrabFocus();
    }

    private void HidePauseMenu()
    {
        GetNode<Button>("HUD/MarginContainer/VBoxContainer/RestartButton").Hide();
        GetNode<Button>("HUD/MarginContainer/VBoxContainer/ResumeButton").Hide();
        GetNode<Button>("HUD/MarginContainer/VBoxContainer/PauseMenuButton").Hide();
    }

    public override void _UnhandledInput(InputEvent inputEvent)
    {
        if (inputEvent.IsActionPressed("pause"))
        {
            if (GetTree().Paused == false)
            {
                GetNode<HUD>("HUD").Pause();
                ShowPauseMenu();
            }
            else
            {
                GetNode<HUD>("HUD").Resume();
                HidePauseMenu();
            }
        }
    }

    private void _on_Player_CoinCollected()
    {
        _score++;
    }

    private void _on_Player_DamageTaken(int numberOfHeartsToHide)
    {
        GetNode<HUD>("HUD").HideNHearts(numberOfHeartsToHide);
    }
}
