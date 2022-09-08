using Godot;

public class SaveProgress : Node
{
    public string LastLevelPlayedPath { get; set; } = "res://scenes/Levels/Core.tscn";

    public bool IsLastLevelCompleted { get; set; } = false;
}
