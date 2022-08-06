using Godot;

public class AboutScene : Control
{
    [Export]
    private string _sceneName = "AboutScene";

    [Signal]
    public delegate void ChangeScene(string nextSceneName);

    public override void _Ready()
    {
        GetNode<Button>("MarginContainer/VBoxContainer/BackButton").GrabFocus();
    }

    public void _on_ContactBtn_button_up()
    {
        OS.ShellOpen("https://t.me/t1vlas");
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventKey eventKey)
        {
            if (eventKey.Pressed && eventKey.Scancode == (int)KeyList.Escape)
            {
                EmitSignal("ChangeScene", "TitleScreen");
            }
        }
    }

    private void _on_BackButton_pressed()
    {
        EmitSignal("ChangeScene", "TitleScreen");
    }
}
