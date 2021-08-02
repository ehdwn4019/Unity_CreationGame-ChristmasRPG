using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    [SerializeField]
    Slider loadingSlider;

    //로딩씬 
    private void Start()
    {
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        yield return null;
        AsyncOperation operation = SceneManager.LoadSceneAsync("InGameScene");
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            yield return null;
            if (loadingSlider.value < 0.9f)
            {
                loadingSlider.value = Mathf.MoveTowards(loadingSlider.value, 0.9f, Time.deltaTime);
            }
            else if (loadingSlider.value >= 0.9f)
            {
                loadingSlider.value = Mathf.MoveTowards(loadingSlider.value, 1f, Time.deltaTime);
            }

            if (loadingSlider.value >= 1f && operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
            }
        }
    }
}
