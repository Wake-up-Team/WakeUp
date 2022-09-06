using Godot;
public class TitleScreen : Control
{
    public override void _Ready()
    {
        var сursor = ResourceLoader.Load("res://Images/Cursor1.png");
        Input.SetCustomMouseCursor(сursor);
        Button playBtn = GetNode<Button>("MarginContainer/VBoxContainer/Buttons/Play/PlayBtn");
        playBtn.GrabFocus();
    }

    private void SwitchScene(string nextScenePath)
    {
        var sceneSwitcher = GetNode<SceneSwitcher>("/root/SceneSwitcher");
        sceneSwitcher.SwitchScene(nextScenePath);
    }

    public void _on_PlayBtn_button_up()
    {
        SwitchScene("res://scenes/Core.tscn");
    }

    public void _on_OptionsBtn_button_up()
    {
        GetNode<Popup>("OptionsMenu").PopupCentered();
    }

    public void _on_AboutBtn_button_up()
    {
        SwitchScene("res://scenes/AboutScene.tscn");
    }

    public void _on_QuitBtn_button_up()
    {
        GetTree().Quit();
    }
}
