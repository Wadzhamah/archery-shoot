using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public string sceneToLoad; // ��� �����, ������� �� ������ ���������
    public Image progressBar; // ������ �� ��������-���

    private AsyncOperation loadingOperation;

    public void Start()
    {
        StartCoroutine(LoadSceneAsync());
    }

    private IEnumerator LoadSceneAsync()
    {
        yield return null;
        loadingOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        loadingOperation.allowSceneActivation = false; // ������������� �������������� ������������ ����

        while (!loadingOperation.isDone)
        {
            float progress = Mathf.Clamp01(loadingOperation.progress / 0.9f); // ����������� �������� �� [0, 1]
            progressBar.fillAmount = progress;

            // ���� �������� �������� ������ 0.9, �� ������ ����� �������� �������������� �������� ��� �������� ��������, ���� ����������

            if (progress >= 0.9f)
            {
                // ����� �� ������ �������� ��� ��� ������ ������ �������� ��� �������� ��������, ���� ����������

                // ��������� ������������ �����
                loadingOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
