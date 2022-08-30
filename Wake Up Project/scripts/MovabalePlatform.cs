using Godot;
using System;

public class MovabalePlatform : Node2D
{
    public override void _Ready()
    {
        Godot.Collections.Array movementPositions = GetNode<Node>("MovementPositions").GetChildren();
        Tween tween = GetNode<Tween>("PlatformBody/Tween");
        KinematicBody2D platformBody = GetNode<KinematicBody2D>("PlatformBody");

    }

}
