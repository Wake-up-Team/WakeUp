using Godot;
using System;

public class HUD : CanvasLayer
{
    [Signal]
    public delegate void RestartGame();

    [Signal]
    public delegate void ChangeScene(string nextSceneName);

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

        var messageTimer = GetNode<Timer>("MessageTimer");

        var message = GetNode<Label>("MarginContainer/VBoxContainer/Message");
        message.Show();

        GetNode<Button>("MarginContainer/VBoxContainer/BackToMenuButton").Show();
        GetNode<Button>("MarginContainer/VBoxContainer/RestartButton").Show();
    }

    private void _on_RestartButton_pressed()
    {
        GetNode<Button>("MarginContainer/VBoxContainer/RestartButton").Hide();
        GetNode<Button>("MarginContainer/VBoxContainer/BackToMenuButton").Hide();
        EmitSignal("RestartGame");
    }

    private void _on_BackToMenuButton_pressed()
    {
        EmitSignal("ChangeScene", "TitleScreen");
    }

    private void _on_MessageTimer_timeout()
    {
        GetNode<Label>("MarginContainer/VBoxContainer/Message").Hide();
    }

}
