using Godot;
using System;

public class Door : Area2D
{
    private AnimationPlayer _animationPlayer;

    [Export]
    private string _pathToTheSceneToWhichTheDoorLeads = "res://scenes/TitleScreen.tscn";

    private bool _theDoorIsOpen = false;
    public override void _Ready()
    {
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
    }

    public void _on_Door_body_entered(Node body)
    {
        if (body is PlayerController)
        {
            _animationPlayer.Play("open");
            GetNode<AudioStreamPlayer2D>("Opening").Play();
        }
    }

    public void _on_Door_body_exited(Node body)
    {
        if (body is PlayerController)
        {
            _animationPlayer.PlayBackwards("open");
            _theDoorIsOpen = false;
        }
    }

    public override void _UnhandledInput(InputEvent inputEvent)
    {
        if (inputEvent is InputEventKey eventKey)
        {
            if (_theDoorIsOpen == true && eventKey.Pressed && eventKey.Scancode == (int)KeyList.J)
            {
                var sceneSwitcher = GetNode<SceneSwitcher>("/root/SceneSwitcher");
                sceneSwitcher.SwitchSceneWithDoor(_pathToTheSceneToWhichTheDoorLeads);
            }
        }
    }

    private void _on_AnimationPlayer_animation_finished(string animationName)
    {
        if (animationName == "open")
        {
            _theDoorIsOpen = true;
        }
    }
}