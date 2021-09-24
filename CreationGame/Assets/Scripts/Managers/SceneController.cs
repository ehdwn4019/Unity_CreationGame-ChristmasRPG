using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public static SceneController instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    //시작씬 이동
    public void GoStartScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("StartScene");
        SoundManager.instance.PlaySoundBgm("시작");
    }

    //현재씬 불러오기 
    public void StayScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SoundManager.instance.PlaySoundBgm("인게임");
    }
}
