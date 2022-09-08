using Godot;

public class TitleScreen : Control
{
    private SceneSwitcher _sceneSwitcher;

    public override void _Ready()
    {
        var сursor = ResourceLoader.Load("res://Images/Cursors/Cursor1.png");
        Input.SetCustomMouseCursor(сursor);
        GetNode<Button>("MarginContainer/VBoxContainer/Buttons/About/AboutBtn").GrabFocus();
        _sceneSwitcher = GetNode<SceneSwitcher>("/root/SceneSwitcher");
        _savedProgress = GetNode<SaveProgress>("/root/SaveProgress");
    }

    private SaveProgress _savedProgress;

    private void _on_PlayBtn_button_up()
    {
        if (_savedProgress.IsLastLevelCompleted)
        {
            GetNode<Popup>("SelectLevelMenu").PopupCentered();
        }
        else
        {
            _sceneSwitcher.SwitchSceneWithElevatorAnimation(_savedProgress.LastLevelPlayedPath);
        }
    }

    private void _on_OptionsBtn_button_up()
    {
        GetNode<Popup>("OptionsMenu").PopupCentered();
    }

    private void _on_AboutBtn_button_up()
    {
        _sceneSwitcher.SwitchScene("res://scenes/UserInterface/AboutScene.tscn");
    }

    private void _on_QuitBtn_button_up()
    {
        GetTree().Quit();
    }
}
