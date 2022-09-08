using Godot;

public class Door : Area2D
{
    [Export]
    private string _pathToTheSceneToWhichTheDoorLeads = "res://scenes/TitleScreen.tscn";

    private AnimationPlayer _animationPlayer;

    public override void _Ready()
    {
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
    }

    private void ShowWhetherTheDoorCanBeUsedByPlayerOrNot(PlayerController player)
    {
        if (player.HasEnoughCoinsToOpenTheDoor)
        {
            GetNode<Label>("Label").Text = "Press J to use the door.";
        }
        else
        {
            GetNode<Label>("Label").Text = "You need more coins.";
        }
        GetNode<Label>("Label").Show();
    }

    private bool _thePlayerHasEnoughCoins = false;
    private bool _theDoorIsOpen = false;

    public void _on_Door_body_entered(Node body)
    {
        if (body is PlayerController player)
        {
            _thePlayerHasEnoughCoins = player.HasEnoughCoinsToOpenTheDoor;
            ShowWhetherTheDoorCanBeUsedByPlayerOrNot(player);
            if (_theDoorIsOpen == false && _thePlayerHasEnoughCoins)
            {
                _animationPlayer.Play("open");
                GetNode<AudioStreamPlayer2D>("Opening").Play();
            }
        }
    }

    public void _on_Door_body_exited(Node body)
    {
        if (body is PlayerController)
        {
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
                sceneSwitcher.SwitchSceneWithElevatorAnimation(_pathToTheSceneToWhichTheDoorLeads);
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
