using Godot;
using System;

public class HUD : CanvasLayer
{
    [Signal]
    public delegate void RestartGame();

    public override void _Ready()
    {
    }

    public void HideAllContent()
    {
        GetNode<Label>("MarginContainer/VBoxContainer/Score").Hide();
        GetNode<Label>("MarginContainer/VBoxContainer/Message").Hide();
        GetNode<Button>("MarginContainer/VBoxContainer/ResumeButton").Hide();
        GetNode<Button>("MarginContainer/VBoxContainer/PauseMenuButton").Hide();
        GetNode<Button>("MarginContainer/VBoxContainer/RestartButton").Hide();
        GetNode<Button>("MarginContainer/VBoxContainer/BackToMenuButton").Hide();
    }

    public void ShowMessage(string text)
    {
        var message = GetNode<Label>("MarginContainer/VBoxContainer/Message");
        message.Text = text;
        message.Show();
        GetNode<Timer>("MessageTimer").Start();
    }

    public void ShowGameOver()
    {
        ShowMessage("Game Over! Try again!");
    }

    public void SetScore(int score)
    {
        GetNode<Label>("MarginContainer/VBoxContainer/Score/ScoreLabel").Text = score.ToString();
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
    }

    private void _on_PauseMenuButton_pressed()
    {
        Resume();
        SwitchScene("res://scenes/TitleScreen.tscn");
    }

    private void _on_RestartButton_pressed()
    {
        EmitSignal("RestartGame");
    }

    private void _on_BackToMenuButton_pressed()
    {
        var sceneSwitcher = GetNode<SceneSwitcher>("/root/SceneSwitcher");
        sceneSwitcher.SwitchScene("res://scenes/TitleScreen.tscn");
    }

    private void _on_MessageTimer_timeout()
    {
        GetNode<Label>("MarginContainer/VBoxContainer/Message").Hide();
    }
}
