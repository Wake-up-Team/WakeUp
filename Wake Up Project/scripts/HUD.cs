using Godot;
using System;

public class HUD : CanvasLayer
{
    [Signal]
    public delegate void RestartGame();

    [Export]
    private string _sceneToRestartPath;

    private TextureRect[] _hpHearts = new TextureRect[3];

    private string[] _gameOverText = new string[]
    {
        "Game Over.",
        "Noob, learn to play and try again.",
        "Cry baby.",
        "Goof.",
        "U are soo dumb! Better delete the game.",
        "Even my grandma plays better.",
        "Call ur mommy and ask her to play for u.",
        "U are a disgrace to the humanity."
    };

    private int _lastVisibleHeartIndex;

    public override void _Ready()
    {
        GD.Randomize();
        _hpHearts[0] = GetNode<TextureRect>("HBoxContainer/heart1");
        _hpHearts[1] = GetNode<TextureRect>("HBoxContainer/heart2");
        _hpHearts[2] = GetNode<TextureRect>("HBoxContainer/heart3");
        _lastVisibleHeartIndex = _hpHearts.Length - 1;
    }

    public void SetScore(int currentScore, int maxScore)
    {
        GetNode<Label>("Score/ScoreLabel").Text = currentScore.ToString() + "/" + maxScore.ToString();
    }

    private void HideHeart(int index)
    {
        _hpHearts[index].Hide();
    }

    public void HideNHearts(int numberOfHeartsToHide)
    {
        for (int i = 0; i < numberOfHeartsToHide && _lastVisibleHeartIndex >= 0; i++)
        {
            HideHeart(_lastVisibleHeartIndex);
            _lastVisibleHeartIndex--;
        }
    }

    public void HideAllContent()
    {
        GetNode<Label>("MarginContainer/VBoxContainer/Message").Hide();
        GetNode<Button>("MarginContainer/VBoxContainer/ResumeButton").Hide();
        GetNode<Button>("MarginContainer/VBoxContainer/PauseMenuButton").Hide();
        GetNode<Button>("MarginContainer/VBoxContainer/RestartButton").Hide();
    }

    public void ShowMessage(string text)
    {
        var message = GetNode<Label>("MarginContainer/VBoxContainer/Message");
        message.Text = text;
        message.Show();
    }

    public void ShowGameOverMessage()
    {
        int index = new Random().Next(0, _gameOverText.Length);
        ShowMessage(_gameOverText[index]);
    }

    private void SwitchScene(string nextScenePath)
    {
        var sceneSwitcher = GetNode<SceneSwitcher>("/root/SceneSwitcher");
        sceneSwitcher.SwitchScene(nextScenePath);
    }

    public void Pause()
    {
        GetTree().Paused = true;
    }

    public void Resume()
    {
        var aim = ResourceLoader.Load("res://Images/aim.png");
        Input.SetCustomMouseCursor(aim);
        GetTree().Paused = false;
    }

    private void _on_ResumeButton_pressed()
    {
        Resume();
        GetNode<Button>("MarginContainer/VBoxContainer/ResumeButton").Hide();
        GetNode<Button>("MarginContainer/VBoxContainer/PauseMenuButton").Hide();
        GetNode<Button>("MarginContainer/VBoxContainer/RestartButton").Hide();
    }

    private void _on_PauseMenuButton_pressed()
    {
        Resume();
        SwitchScene("res://scenes/TitleScreen.tscn");
    }

    private void _on_RestartButton_pressed()
    {
        if (GetTree().Paused)
        {
            Resume();
        }
        SwitchScene(_sceneToRestartPath);
    }

    private void _on_BackToMenuButton_pressed()
    {
        var sceneSwitcher = GetNode<SceneSwitcher>("/root/SceneSwitcher");
        sceneSwitcher.SwitchScene("res://scenes/TitleScreen.tscn");
    }
}
