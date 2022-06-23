using Godot;
using System;

public class WorldEnvironmentScript : WorldEnvironment
{
    GlobalOptions globalOptions;

    public WorldEnvironmentScript()
    {
        globalOptions = GlobalOptions.GetInstance;
    }

    public override void _Ready()
    {

    }
}
