using Godot;

public class SceneSwitcher : Node
{
    public Node CurrentScene { get; set; }

    public override void _Ready()
    {
        Viewport root = GetTree().Root;
        CurrentScene = root.GetChild(root.GetChildCount() - 1);
    }

    public async void SwitchScene(string nextScenePath)
    {
        CurrentScene.QueueFree();
        await ToSignal(GetTree().CreateTimer(0.00000001f), "timeout");
        var nextScene = (PackedScene)GD.Load(nextScenePath);
        CurrentScene = nextScene.Instance();
        GetTree().Root.AddChild(CurrentScene);
        GetTree().CurrentScene = CurrentScene;
    }
}
