using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public string sceneToLoad; // Имя сцены, которую вы хотите загрузить
    public Image progressBar; // Ссылка на прогресс-бар

    private AsyncOperation loadingOperation;

    public void Start()
    {
        StartCoroutine(LoadSceneAsync());
    }

    private IEnumerator LoadSceneAsync()
    {
        yield return null;
        loadingOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        loadingOperation.allowSceneActivation = false; // Предотвращаем автоматическое переключение сцен

        while (!loadingOperation.isDone)
        {
            float progress = Mathf.Clamp01(loadingOperation.progress / 0.9f); // Нормализуем прогресс до [0, 1]
            progressBar.fillAmount = progress;

            // Если прогресс загрузки достиг 0.9, вы можете здесь добавить дополнительные задержки или анимации загрузки, если необходимо

            if (progress >= 0.9f)
            {
                // Здесь вы можете добавить код для показа экрана загрузки или анимации загрузки, если необходимо

                // Разрешаем переключение сцены
                loadingOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
