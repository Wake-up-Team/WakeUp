using Godot;

public class AboutScene : Control
{
    public override void _Ready()
    {
        GetNode<Button>("MarginContainer/VBoxContainer/BackButton").GrabFocus();
    }

    private void SwitchScene(string nextScenePath)
    {
        var sceneSwitcher = GetNode<SceneSwitcher>("/root/SceneSwitcher");
        sceneSwitcher.SwitchScene(nextScenePath);
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
                SwitchScene("res://scenes/TitleScreen.tscn");
            }
        }
    }

    private void _on_BackButton_pressed()
    {
        SwitchScene("res://scenes/TitleScreen.tscn");
    }
}
