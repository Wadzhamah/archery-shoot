using UnityEngine;
using UnityEngine.UI;

public class StartGamePopup : BaseScreen
{
    [SerializeField]
    private Button _startGameButton;

    private void Awake()
    {
        _startGameButton.onClick.AddListener(OnStartGameButtonClick);
    }

    private void OnStartGameButtonClick()
    {
        GameController.Instance.Init();
        this.Close();
    }
}
