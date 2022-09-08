using Godot;
using System.Collections.Generic;

public class SceneSwitcher : Node
{
    private Node _currentScene { get; set; }

    public override void _Ready()
    {
        Viewport root = GetTree().Root;
        _currentScene = root.GetChild(root.GetChildCount() - 1);
    }

    private List<string> _levelsPaths = new List<string>(){
        "res://scenes/Levels/Core.tscn",
        "res://scenes/Levels/Core2.tscn",
        "res://scenes/Levels/Core3.tscn"
    };

    public async void SwitchScene(string nextScenePath)
    {
        if (_currentScene != null)
        {
            _currentScene.QueueFree();
        }
        await ToSignal(GetTree().CreateTimer(0.00000001f), "timeout");
        var nextScene = (PackedScene)GD.Load(nextScenePath);
        _currentScene = nextScene.Instance();
        GetTree().Root.AddChild(_currentScene);
        GetTree().CurrentScene = _currentScene;

        if (_levelsPaths.Contains(nextScenePath))
        {
            SaveProgress.GetInstance.LastLevelPlayedPath = nextScenePath;
        }
    }

    public async void SwitchSceneWithElevatorAnimation(string nextScenePath = "res://scenes/UserInterface/TitleScreen.tscn")
    {
        _currentScene.QueueFree();
        _currentScene = null;

        var elevatorScene = (PackedScene)GD.Load("res://scenes/Other/ElevatorIdling.tscn");
        var elevatorInstance = elevatorScene.Instance();
        GetTree().Root.AddChild(elevatorInstance);
        await ToSignal(GetTree().CreateTimer(6), "timeout");

        elevatorInstance.QueueFree();
        SwitchScene(nextScenePath);
    }
}
