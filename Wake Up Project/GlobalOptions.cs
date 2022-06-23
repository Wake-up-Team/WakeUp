using Godot;
using System;

public class GlobalOptions : Node
{
    [Signal]
    public delegate void brightness_changed(float value);

    private static GlobalOptions instance = new GlobalOptions();
    private GlobalOptions() { }
    public static GlobalOptions GetInstance
    {
        get
        {
            return instance;
        }
    }

    public override void _Ready()
    {
    }

    public void FullScreen(bool value)
    {
        OS.WindowFullscreen = value;
        if (!value)
        {
            OS.WindowSize = new Vector2(1280, 720);
            OS.WindowPosition = new Vector2((OS.GetScreenSize().x - OS.WindowSize.x) / 2, (OS.GetScreenSize().y - OS.WindowSize.y) / 2);
        }
    }

    public void VSync(bool value)
    {
        OS.VsyncEnabled = value;
    }

    public void Brightness(float value)
    {

    }

    public void Volume(float value)
    {
        AudioServer.SetBusVolumeDb(0, value);
    }
}
