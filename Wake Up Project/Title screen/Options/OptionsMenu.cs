using Godot;
using System;

public class OptionsMenu : Popup
{
    GlobalOptions globalOptions;

    public OptionsMenu()
    {
        globalOptions = GlobalOptions.GetInstance;
    }
    // video settings
    public override void _Ready()
    {

    }

    public void _on_DisplayModeBtn_item_selected(int index)
    {
        globalOptions.FullScreen(index == 1);
    }

    public void _on_VsyncBtn_toggled(bool buttonIsPressed)
    {
        globalOptions.VSync(buttonIsPressed);
    }

    public void _on_BrightnessSlider_value_changed(float value)
    {
        globalOptions.Brightness(value);
    }

    // audio settings
    public void _on_VolumeSlider_value_changed(float value)
    {
        globalOptions.Volume(value);
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
