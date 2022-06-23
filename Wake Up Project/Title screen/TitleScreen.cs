using Godot;
using System;

public class TitleScreen : Control
{

    public override void _Ready()
    {
        var first = GetNode<MarginContainer>("MarginContainer");
        var second = first.GetNode<VBoxContainer>("VBoxContainer");
        var third = second.GetNode<VBoxContainer>("Buttons");
        var fourth = third.GetNode<MarginContainer>("Play");
        var fifth = fourth.GetNode<Button>("PlayBtn");
        fifth.GrabFocus();
    }

    public void _on_PlayBtn_button_up()
    {
        GetTree().ChangeScene("res://Core.tscn");
    }

    public void _on_OptionsBtn_button_up()
    {
        GetNode<Popup>("OptionsMenu").PopupCentered();
    }

    public void _on_AboutBtn_button_up()
    {
        GetTree().ChangeScene("res://Title screen/About the game/AboutScene.tscn");
    }

    public void _on_QuitBtn_button_up()
    {
        GetTree().Quit();
    }
}
