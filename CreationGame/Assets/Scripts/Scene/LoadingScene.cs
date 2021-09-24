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

        //준비 될때까지 반복
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
                SoundManager.instance.PlaySoundBgm("인게임");
                operation.allowSceneActivation = true;
            }
        }
    }
}
