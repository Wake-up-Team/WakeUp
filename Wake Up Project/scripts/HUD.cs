using Godot;
using System;

public class HUD : CanvasLayer
{
    [Signal]
    public delegate void RestartGame();

    private TextureRect[] _hpHearts = new TextureRect[3];

    private int _lastVisibleHeartIndex;

    public override void _Ready()
    {
        _hpHearts[0] = GetNode<TextureRect>("HBoxContainer/heart1");
        _hpHearts[1] = GetNode<TextureRect>("HBoxContainer/heart2");
        _hpHearts[2] = GetNode<TextureRect>("HBoxContainer/heart3");
        _lastVisibleHeartIndex = _hpHearts.Length - 1;
    }

    private void HideHeart(int index)
    {
        _hpHearts[index].Hide();
    }

    public void HideNHearts(int numberOfHeartsToHide)
    {
        for (int i = 0; i < numberOfHeartsToHide && _lastVisibleHeartIndex >= 0; i++)
        {
            HideHeart(_lastVisibleHeartIndex);
            _lastVisibleHeartIndex--;
        }
    }

    public void HideAllContent()
    {
        GetNode<Label>("MarginContainer/VBoxContainer/Message").Hide();
        GetNode<Button>("MarginContainer/VBoxContainer/ResumeButton").Hide();
        GetNode<Button>("MarginContainer/VBoxContainer/PauseMenuButton").Hide();
        GetNode<Button>("MarginContainer/VBoxContainer/RestartButton").Hide();
    }

    public void ShowMessage(string text)
    {
        var message = GetNode<Label>("MarginContainer/VBoxContainer/Message");
        message.Text = text;
        message.Show();
    }

    public void ShowGameOver()
    {
        ShowMessage("Game Over! Try again!");
    }

    private void SwitchScene(string nextScenePath)
    {
        var sceneSwitcher = GetNode<SceneSwitcher>("/root/SceneSwitcher");
        sceneSwitcher.SwitchScene(nextScenePath);
    }

    public void Pause()
    {
        GetTree().Paused = true;
    }

    public void Resume()
    {
        GetTree().Paused = false;
    }

    private void _on_ResumeButton_pressed()
    {
        Resume();
        GetNode<Button>("MarginContainer/VBoxContainer/ResumeButton").Hide();
        GetNode<Button>("MarginContainer/VBoxContainer/PauseMenuButton").Hide();
        GetNode<Button>("MarginContainer/VBoxContainer/RestartButton").Hide();
    }

    private void _on_PauseMenuButton_pressed()
    {
        Resume();
        SwitchScene("res://scenes/TitleScreen.tscn");
    }

    private void _on_RestartButton_pressed()
    {
        if (GetTree().Paused)
        {
            Resume();
        }
        EmitSignal("RestartGame");
    }

    private void _on_BackToMenuButton_pressed()
    {
        var sceneSwitcher = GetNode<SceneSwitcher>("/root/SceneSwitcher");
        sceneSwitcher.SwitchScene("res://scenes/TitleScreen.tscn");
    }
}
