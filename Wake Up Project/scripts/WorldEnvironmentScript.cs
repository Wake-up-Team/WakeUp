using Godot;

public class WorldEnvironmentScript : WorldEnvironment
{
    private GlobalOptions _globalOptions;

    public WorldEnvironmentScript()
    {
        _globalOptions = GlobalOptions.GetInstance;
    }

    public override void _Ready()
    {

    }
}
