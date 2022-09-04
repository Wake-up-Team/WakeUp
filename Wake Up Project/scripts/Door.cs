using Godot;
using System;

public class Door : Area2D
{
    private AnimationPlayer _animationPlayer;

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
            _theDoorIsOpen = true;
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
            if (_theDoorIsOpen && eventKey.Pressed && eventKey.Scancode == (int)KeyList.J)
            {
                var sceneSwitcher = GetNode<SceneSwitcher>("/root/SceneSwitcher");
                sceneSwitcher.SwitchScene("res://scenes/TitleScreen.tscn");
            }
        }
    }
}
