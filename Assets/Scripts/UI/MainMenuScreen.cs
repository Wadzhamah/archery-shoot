using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScreen : BaseScreen
{
    [SerializeField]
    private Button _playButton;
    [SerializeField]
    private Button _settingsButton;
    [SerializeField]
    private Button _exitButton;

    private void Awake()
    {
        _playButton.onClick.AddListener(OnPlayButtonClick);
        _settingsButton.onClick.AddListener(OnSettingsButtonClick);
        _exitButton.onClick.AddListener(OnExitButtonClick);
    }

    private void OnPlayButtonClick()
    {
        
    }

    private void OnSettingsButtonClick()
    {
        throw new NotImplementedException();
    }

    private void OnExitButtonClick()
    {
        Application.Quit();
    }
}
