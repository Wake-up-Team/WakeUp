using Godot;

public class TitleScreen : Control
{

    public override void _Ready()
    {
        Button playBtn = GetNode<Button>("MarginContainer/VBoxContainer/Buttons/Play/PlayBtn");
        playBtn.GrabFocus();
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
