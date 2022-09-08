using Godot;

public class SelectLevelMenu : PopupMenu
{
    private void SwitchScene(string nextScenePath)
    {
        var sceneSwitcher = GetNode<SceneSwitcher>("/root/SceneSwitcher");
        sceneSwitcher.SwitchSceneWithElevatorAnimation(nextScenePath);
    }

    private void _on_Level1Button_pressed()
    {
        SwitchScene("res://scenes/Levels/Core.tscn");
    }

    private void _on_Level2Button_pressed()
    {
        SwitchScene("res://scenes/Levels/Core2.tscn");
    }

    private void _on_Level3Button_pressed()
    {
        SwitchScene("res://scenes/Levels/Core3.tscn");
    }
}