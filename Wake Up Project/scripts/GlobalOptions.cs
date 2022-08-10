using Godot;

public class GlobalOptions : Node
{
    [Signal]
    public delegate void BrightnessChanged(float value);

    private static GlobalOptions s_instance = new GlobalOptions();
    private GlobalOptions() { }
    public static GlobalOptions GetInstance
    {
        get
        {
            return s_instance;
        }
    }

    public void UpdateDisplayMode(bool shouldBeFullScreen)
    {
        OS.WindowFullscreen = shouldBeFullScreen;
        if (!shouldBeFullScreen)
        {
            OS.WindowSize = new Vector2(1280, 720);
            OS.WindowPosition = new Vector2((OS.GetScreenSize().x - OS.WindowSize.x) / 2, (OS.GetScreenSize().y - OS.WindowSize.y) / 2);
        }
    }

    public void EnableVsync(bool turnOnVsync)
    {
        OS.VsyncEnabled = turnOnVsync;
    }

    public void UpdateVolume(float value)
    {
        AudioServer.SetBusVolumeDb(0, value);
    }
}
