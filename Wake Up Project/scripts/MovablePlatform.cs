using Godot;
using Godot.Collections;

public class MovablePlatform : Node2D
{

    private Tween tween;

    private int indexOfPosition;

    private KinematicBody2D platformBody;

    private Array movementPositions;

    public override void _Ready()
    {
        movementPositions = GetNode<Node>("MovementPositions").GetChildren();
        tween = GetNode<Tween>("PlatformBody/Tween");
        platformBody = GetNode<KinematicBody2D>("PlatformBody");
        _on_Tween_tween_completed(null, null);
    }

    private void _on_Tween_tween_completed(object obj, NodePath path)
    {
        indexOfPosition++;
        if (indexOfPosition > movementPositions.Count - 1)
        {
            indexOfPosition = 0;
        }
        Position2D movePosition = movementPositions[indexOfPosition] as Position2D;
        int moveSpeed = 30;
        tween.InterpolateProperty(platformBody, "position", platformBody.Position, movePosition.Position, movePosition.Position.DistanceTo(platformBody.Position) / moveSpeed, Tween.TransitionType.Linear, Tween.EaseType.InOut);
        tween.Start();
    }
}
