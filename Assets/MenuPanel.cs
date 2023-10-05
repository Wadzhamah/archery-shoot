using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _userNameHolder;
    [SerializeField]
    private TMP_InputField _userNameInput;

    [SerializeField]
    private Button _playButton;
    [SerializeField]
    private Button _infoButton;
    [SerializeField]
    private Button _exitButton;

    [SerializeField]
    private int maxUsernameLength = 10;

    private Image _tapToPlayImage;

    private SoundManager _soundManager;

    private void Awake()
    {
        _playButton.onClick.AddListener(OnPlayButtonClick);
        _infoButton.onClick.AddListener(OnInfoButtonClick);
        _exitButton.onClick.AddListener(OnExitButtonClick);

        _userNameInput.onEndEdit.AddListener(OnEndUsernameEdit);
        _userNameInput.characterLimit = maxUsernameLength;

        _tapToPlayImage = _playButton.gameObject.GetComponent<Image>();
        AnimateTapToPlayImage();
        _userNameHolder.text = GlobalVariables.UserName;

        _soundManager = SoundManager.Instance;

        Application.targetFrameRate = 60;
    }

    private void OnEndUsernameEdit(string arg0)
    {
        GlobalVariables.UserName = arg0;

        Debug.Log(GlobalVariables.UserName);
    }

    private void OnPlayButtonClick()
    {
        //SoundManager.PlaySfx(_soundManager.soundClick);

        SceneManager.LoadScene("Game");
    }

    private void AnimateTapToPlayImage()
    {
        _tapToPlayImage.DOColor(new Color(1f, 1f, 1f, 0.25f), 1f) // Змінює колір тексту на прозорий за 1 секунду.
            .SetLoops(-1, LoopType.Yoyo); // Запускає зациклення анімації, щоб текст блимав вперед і назад.
    }

    private void OnInfoButtonClick()
    {
        SoundManager.PlaySfx(_soundManager.soundClick);

        Application.OpenURL(GlobalVariables.INFO_URL);
    }

    private void OnExitButtonClick()
    {
        SoundManager.PlaySfx(_soundManager.soundClick);

        Application.Quit();
    }
}
