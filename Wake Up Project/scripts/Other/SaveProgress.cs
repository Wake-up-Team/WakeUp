using Godot;

public class SaveProgress : Node
{
    private SaveProgress() { }

    private static SaveProgress s_instance = new SaveProgress();
    public static SaveProgress GetInstance
    {
        get
        {
            return s_instance;
        }
    }

    private string _lastLevelPlayedPath = "res://scenes/Levels/Core.tscn";

    public string LastLevelPlayedPath
    {
        get => _lastLevelPlayedPath;
        set
        {
            _lastLevelPlayedPath = value;
        }
    }

    public bool IsLastLevelCompleted { get; set; } = false;
}
