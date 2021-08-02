using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    //스타트 버튼 클릭시
    public void StartBtn()
    {
        SceneManager.LoadScene("LoadingScene");
        //GameManager.instance.CreateManagers();
    }

    //나가기 버튼 클릭시 
    public void ExitBtn()
    {
        Application.Quit();
    }
}
