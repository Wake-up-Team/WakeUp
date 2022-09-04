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
        if (CurrentScene != null)
        {
            CurrentScene.QueueFree();
        }
        await ToSignal(GetTree().CreateTimer(0.00000001f), "timeout");
        var nextScene = (PackedScene)GD.Load(nextScenePath);
        CurrentScene = nextScene.Instance();
        GetTree().Root.AddChild(CurrentScene);
        GetTree().CurrentScene = CurrentScene;
    }

    public async void SwitchSceneWithDoor(string nextScenePath = "res://scenes/TitleScreen.tscn")
    {
        CurrentScene.QueueFree();
        CurrentScene = null;

        var elevatorScene = (PackedScene)GD.Load("res://scenes/ElevatorIdling.tscn");
        var elevatorInstance = elevatorScene.Instance();
        GetTree().Root.AddChild(elevatorInstance);
        await ToSignal(GetTree().CreateTimer(6), "timeout");

        elevatorInstance.QueueFree();
        SwitchScene(nextScenePath);
    }
}
