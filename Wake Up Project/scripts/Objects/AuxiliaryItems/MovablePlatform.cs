using Godot;
using Godot.Collections;

public class MovablePlatform : Node2D
{
    private Tween _tween;
    private KinematicBody2D _platformBody;
    private Array _movementPositions;

    public override void _Ready()
    {
        _movementPositions = GetNode<Node>("MovementPositions").GetChildren();
        _tween = GetNode<Tween>("PlatformBody/Tween");
        _platformBody = GetNode<KinematicBody2D>("PlatformBody");
        _on_Tween_tween_completed(null, null);
    }

    private int _indexOfPosition;

    private void _on_Tween_tween_completed(object obj, NodePath path)
    {
        _indexOfPosition++;
        if (_indexOfPosition > _movementPositions.Count - 1)
        {
            _indexOfPosition = 0;
        }
        Position2D movePosition = _movementPositions[_indexOfPosition] as Position2D;
        int moveSpeed = 30;
        _tween.InterpolateProperty(_platformBody, "position", _platformBody.Position, movePosition.Position, movePosition.Position.DistanceTo(_platformBody.Position) / moveSpeed, Tween.TransitionType.Linear, Tween.EaseType.InOut);
        _tween.Start();
    }
}
