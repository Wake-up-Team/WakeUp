using Godot;
using System;

public class Core2 : Node2D
{
    [Export]
    public Position2D RespPos;
    private int _score = 0;

    private int _numberOfCoinsToUnlockTheDoor;

    private void HideAllHudContent()
    {
        GetNode<HUD>("HUD").HideAllContent();
    }

    private void SetScoreInHUD(int score)
    {
        GetNode<HUD>("HUD").SetScore(score, _numberOfCoinsToUnlockTheDoor);
    }

    public override void _Ready()
    {
        HideAllHudContent();
        _numberOfCoinsToUnlockTheDoor = GetNode<Node2D>("Coins").GetChildCount();
    }

    private void ShowPosthumousMenu()
    {
        GetNode<Button>("HUD/MarginContainer/VBoxContainer/RestartButton").Show();
        GetNode<Button>("HUD/MarginContainer/VBoxContainer/PauseMenuButton").Show();
        GetNode<Button>("HUD/MarginContainer/VBoxContainer/RestartButton").GrabFocus();
    }

    private void _on_Player_Death()
    {
        ShowPosthumousMenu();
        GetNode<HUD>("HUD").ShowGameOverMessage();
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
        SetScoreInHUD(_score);
        if (_score == _numberOfCoinsToUnlockTheDoor)
        {
            GetNode<PlayerController>("Player").HasEnoughCoinsToOpenTheDoor = true;
        }
    }

    private void _on_Player_DamageTaken(int numberOfHeartsToHide)
    {
        GetNode<HUD>("HUD").HideNHearts(numberOfHeartsToHide);
    }
}
