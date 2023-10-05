using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinGamePopup : BaseScreen
{
    [SerializeField]
    private Button _replayButton;

    [SerializeField]
    GameObject _border;

    private GameController _gameController;

    private void GameController_OnGameFinish()
    {
        Open();
    }

    private void OnReplayButtonClick()
    {
        _gameController.StartNewGame();
        this.Close();
    }

    public override void Init()
    {
        _gameController = GameController.Instance;
        _gameController.OnGameFinish += GameController_OnGameFinish;
        _replayButton.onClick.AddListener(OnReplayButtonClick);
    }

    public override void Open()
    {
        base.Open();
        _border.SetActive(false);
    }

    public override void Close()
    {
        base.Close();
        _border.SetActive(true);
    }
}
