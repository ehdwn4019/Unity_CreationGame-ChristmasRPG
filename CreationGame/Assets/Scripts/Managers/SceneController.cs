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

    //public enum SceneType
    //{
    //    None,
    //    StartScene,
    //    LoadingScene,
    //    InGameScene,
    //}
    //
    //SceneType st = SceneType.None; 
    //public SceneType St { get { return st; } set { st = value; } }
    //
    //public Dictionary<int, string> sceneList = new Dictionary<int, string>();

    //시작씬 이동
    public void GoStartScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("StartScene");
    }

    //현재씬 불러오기 
    public void StayScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //void AddSceneList()
    //{
    //    sceneList[0] = "StartScene";
    //    sceneList[1] = "LoadingScene";
    //    sceneList[2] = "InGameScene";
    //
    //    foreach (var e in sceneList)
    //        Debug.Log(e);
    //}
}
