using Godot;

public class AboutScene : Control
{
    public override void _Ready()
    {

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
                GetTree().ChangeScene("res://scenes/TitleScreen.tscn");
            }
        }
    }
}
