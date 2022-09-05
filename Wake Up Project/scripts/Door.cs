using Godot;
using System;

public class Door : Area2D
{
    private AnimationPlayer _animationPlayer;

    [Export]
    private string _pathToTheSceneToWhichTheDoorLeads = "res://scenes/TitleScreen.tscn";

    private bool _theDoorIsOpen = false;
    private bool _thePlayerHasEnoughCoins = false;
    public override void _Ready()
    {
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
    }

    private void SetTextToLabel(bool isAllowedToUseTheDoor)
    {
        if (isAllowedToUseTheDoor)
        {
            GetNode<Label>("Label").Text = "Press J to use the door.";
        }
        else
        {
            GetNode<Label>("Label").Text = "You need more coins.";
        }
    }

    public void _on_Door_body_entered(Node body)
    {
        if (body is PlayerController player)
        {
            _thePlayerHasEnoughCoins = player.HasEnoughCoinsToOpenTheDoor;
            _animationPlayer.Play("open");
            GetNode<AudioStreamPlayer2D>("Opening").Play();
            SetTextToLabel(_thePlayerHasEnoughCoins);
            GetNode<Label>("Label").Show();
        }
    }

    public void _on_Door_body_exited(Node body)
    {
        if (body is PlayerController)
        {
            _animationPlayer.PlayBackwards("open");
            _theDoorIsOpen = false;
            GetNode<Label>("Label").Hide();
        }
    }

    private bool IsAllowedToUseTheDoor()
    {
        return _theDoorIsOpen && _thePlayerHasEnoughCoins;
    }

    public override void _UnhandledInput(InputEvent inputEvent)
    {
        if (inputEvent is InputEventKey eventKey)
        {
            if (IsAllowedToUseTheDoor() && eventKey.Pressed && eventKey.Scancode == (int)KeyList.J)
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
