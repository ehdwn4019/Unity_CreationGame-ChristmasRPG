using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;

    [SerializeField] GameObject option;

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

    public void ClickMenu()
    {
        option.SetActive(true);
        Time.timeScale = 0;
    }

    public void ClickHome()
    {
        SceneController.instance.GoStartScene();
    }

    public void ClickReTry()
    {
        SceneController.instance.StayScene();
    }

    public void ClickContinue()
    {
        option.SetActive(false);
        Time.timeScale = 1;
    }

    public void EndUI()
    {

    }
}
