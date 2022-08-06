using Godot;
using System.Collections.Generic;

public class SceneSwitcher : Node
{
    private Node _currentScene;

    private Dictionary<string, string> _sceneNameAndPath = new Dictionary<string, string>{
        {"TitleScreen", "res://scenes/TitleScreen.tscn"},
        {"Core", "res://scenes/Core.tscn"},
        {"AboutScene", "res://scenes/AboutScene.tscn"},
    };

    public override void _Ready()
    {
        _currentScene = GetNode<Control>("TitleScreen");
    }

    private void _on_ChangeScene(string nextSceneName)
    {
        if (_sceneNameAndPath.ContainsKey(nextSceneName))
        {
            string nextScenePath = _sceneNameAndPath[nextSceneName];
            var nextScene = ((PackedScene)ResourceLoader.Load(nextScenePath)).Instance();
            _currentScene.QueueFree();
            _currentScene = nextScene;
            AddChild(_currentScene);
            _currentScene.Connect("ChangeScene", this, nameof(_on_ChangeScene));
        }
    }
}
