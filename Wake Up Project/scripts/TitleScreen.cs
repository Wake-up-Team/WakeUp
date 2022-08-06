using Godot;

public class TitleScreen : Control
{

    [Signal]
    public delegate void ChangeScene(string nextSceneName);
    [Export]
    private string _sceneName = "TitleScreen";
    public override void _Ready()
    {
        Button playBtn = GetNode<Button>("MarginContainer/VBoxContainer/Buttons/Play/PlayBtn");
        playBtn.GrabFocus();
    }

    public void _on_PlayBtn_button_up()
    {
        EmitSignal("ChangeScene", "Core");
    }

    public void _on_OptionsBtn_button_up()
    {
        GetNode<Popup>("OptionsMenu").PopupCentered();
    }

    public void _on_AboutBtn_button_up()
    {
        EmitSignal("ChangeScene", "AboutScene");
    }

    public void _on_QuitBtn_button_up()
    {
        GetTree().Quit();
    }
}
