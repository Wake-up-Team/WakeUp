using Godot;
using System;

public class HUD : CanvasLayer
{
    [Signal]
    public delegate void RestartGame();

    public override void _Ready()
    {
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
        GetNode<Label>("MarginContainer/VBoxContainer/Score").Text = "Score: " + score.ToString();
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
