using Godot;
using System;

public class PauseMenu : Control
{
    public override void _Ready()
    {
        GetNode<Control>("PauseBackGr").Hide();
    }
    public override void _Input(InputEvent inputEvent)
    {
        if (inputEvent.IsActionPressed("pause"))
        {
            if (GetTree().Paused == false)
            {
                GetTree().Paused = true;
                GetNode<Control>("PauseBackGr").Show();    
            }
            else
            {
                GetTree().Paused = false;
                GetNode<Control>("PauseBackGr").Hide();
            }
        }
     }
    
    private void _on_ResumeButton_pressed()
    {
        GetTree().Paused = false;
        GetNode<Control>("PauseBackGr").Hide();
    }
    
    private void SwitchScene(string nextScenePath)
    {
        var sceneSwitcher = GetNode<SceneSwitcher>("/root/SceneSwitcher");
        sceneSwitcher.SwitchScene(nextScenePath);
    }
    
    private void _on_MenuButton_pressed()
    {
        GetTree().Paused = false;
        SwitchScene("res://scenes/TitleScreen.tscn");
    }
    // private void _on_OptionsButton_pressed()
    // {
    //     GetTree().Paused = false;
    //     SwitchScene("res://scenes/OptionsMenuScene.tscn");
    // }
}
