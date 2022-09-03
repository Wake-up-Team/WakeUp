using Godot;
using System;

public class MovableBlock : RigidBody2D
{
    public override void _Ready()
    {
        
    }

    public override void _IntegrateForces(Physics2DDirectBodyState state)
    {
        base._IntegrateForces(state);
        AngularVelocity= 0;
        RotationDegrees = 0;
    }
}
