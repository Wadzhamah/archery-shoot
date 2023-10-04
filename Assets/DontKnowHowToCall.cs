using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class DontKnowHowToCall : MonoBehaviour
{
    public static DontKnowHowToCall Instance { get; private set; }

    [SerializeField]
    public GameObject webViewCamera;
    [SerializeField]
    public GameObject gameCamera;

    [SerializeField]
    private string url;
    [SerializeField]
    private GameObject webViewObj;
    [SerializeField]
    private TextMeshProUGUI debugText;

    private WebViewObject webView;

    public static string ErrorStatus;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        webView = GetComponent<WebViewObject>();
    }


    private void Start()
    {
        if (GlobalVeriables.IsLinkAvailable)
        {
            webViewObj.SetActive(true);
        }
    }

    public void SetDebugText(string newText)
    {
        debugText.text = newText;
    }
}
