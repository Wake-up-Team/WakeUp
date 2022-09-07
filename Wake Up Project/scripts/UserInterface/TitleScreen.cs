using Godot;

public class TitleScreen : Control
{
    private SceneSwitcher _sceneSwitcher;

    public override void _Ready()
    {
        var сursor = ResourceLoader.Load("res://Images/Cursor1.png");
        Input.SetCustomMouseCursor(сursor);
        GetNode<Button>("MarginContainer/VBoxContainer/Buttons/About/AboutBtn").GrabFocus();
        _sceneSwitcher = GetNode<SceneSwitcher>("/root/SceneSwitcher");
    }

    public void _on_PlayBtn_button_up()
    {
        _sceneSwitcher.SwitchSceneWithElevatorAnimation("res://scenes/Levels/Core.tscn");
    }

    public void _on_OptionsBtn_button_up()
    {
        GetNode<Popup>("OptionsMenu").PopupCentered();
    }

    public void _on_AboutBtn_button_up()
    {
        _sceneSwitcher.SwitchScene("res://scenes/UserInterface/AboutScene.tscn");
    }

    public void _on_QuitBtn_button_up()
    {
        GetTree().Quit();
    }
}
