using Godot;

public class OptionsMenu : Popup
{
    private GlobalOptions _globalOptions;

    public OptionsMenu()
    {
        _globalOptions = GlobalOptions.GetInstance;
    }

    private void _on_DisplayModeBtn_item_selected(int index)
    {
        _globalOptions.UpdateDisplayMode(index == 1);
    }

    private void _on_VsyncBtn_toggled(bool buttonIsPressed)
    {
        _globalOptions.EnableVsync(buttonIsPressed);
    }

    private void _on_TurnOffBtn_toggled(bool buttonIsPressed)
    {
        _globalOptions.TurnOffTheSound(buttonIsPressed);
    }

    private void _on_VolumeSlider_value_changed(float value)
    {
        _globalOptions.UpdateVolume(value);
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventKey eventKey)
        {
            if (eventKey.Pressed && eventKey.Scancode == (int)KeyList.Escape)
            {
                Hide();
            }
        }
    }
}
