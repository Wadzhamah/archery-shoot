using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoadingPanel : MonoBehaviour
{
    [SerializeField]
    private SampleWebView mWebView;
    [SerializeField] private Image _loadingBar;
    [SerializeField] private float _fakeLoadingTimeMin = 2f;
    [SerializeField] private float _fakeLoadingTimeMax = 4f;

    private void Start()
    {
        StartCoroutine(FakeLoading());
    }

    IEnumerator FakeLoading()
    {
        yield return null;

        GameObject Webview = GameObject.Find("WebViewObject");
        if (Webview != null)
        {
            Webview.GetComponent<WebViewObject>().SetVisibility(false);
        }

        // Generate a random fake loading time between min and max values.
        float fakeLoadTime = Random.Range(_fakeLoadingTimeMin, _fakeLoadingTimeMax);
        float elapsedTime = 0f;

        while (elapsedTime < fakeLoadTime)
        {
            // Calculate the progress as a ratio of elapsed time to total loading time.
            float progress = Mathf.Clamp01(elapsedTime / fakeLoadTime);

            // Set the loading bar fill amount to simulate loading progress.
            _loadingBar.fillAmount = progress;

            // Increase the elapsed time.
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Ensure the loading bar is at 100%.
        _loadingBar.fillAmount = 1f;

        if (GlobalVeriables.IsLinkAvailable)
        {
            Webview = GameObject.Find("WebViewObject");
            if (Webview != null)
            {
                Webview.GetComponent<WebViewObject>().SetVisibility(true);
            }
            SampleWebView.IsWebViewActive = true;
        }
        gameObject.SetActive(false);


        // You can add any additional actions or transitions here.
    }


}
