using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.LowLevel;
using UnityEngine.UI;

public class GamePanel : BaseScreen
{
    [SerializeField]
    private Button _backButton;
    [SerializeField]
    private Button _exitButton;
    [SerializeField]
    private Button _replayButton;
    [SerializeField]
    private Button _infoButton;

    private SoundManager _soundManager;

    private void Awake()
    {
        _exitButton.onClick.AddListener(OnExitButtonClick);
        _backButton.onClick.AddListener(OnBackButtonClick);
        _replayButton.onClick.AddListener(OnReaplyButtonClick);
        _infoButton.onClick.AddListener(OnInfoButtonClick);

        _soundManager = FindObjectOfType<SoundManager>();
    }

    private void OnExitButtonClick()
    {
        SoundManager.PlaySfx(_soundManager.soundClick);

        Application.Quit();
    }

    private void OnBackButtonClick()
    {
        //SoundManager.PlaySfx(_soundManager.soundClick);

        SceneManager.LoadScene("Menu");
    }

    private void OnReaplyButtonClick()
    {
        SoundManager.PlaySfx(_soundManager.soundClick);

        GameController.Instance.StartNewGame();
    }

    private void OnInfoButtonClick()
    {
        SoundManager.PlaySfx(_soundManager.soundClick);

        Application.OpenURL(GlobalVariables.INFO_URL);
    }
}
