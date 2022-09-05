using Godot;
using System;

public class MovableBlock : RigidBody2D
{
    public override void _Ready()
    {
        
    }

    public override void _IntegrateForces(Physics2DDirectBodyState state)
    {
        //base._IntegrateForces(state);
        //AngularDamp = 0;
        //AngularVelocity= 0;
        //RotationDegrees = 0;
        //Friction = 0.4f;
        //Inertia = 0;
    }
}
