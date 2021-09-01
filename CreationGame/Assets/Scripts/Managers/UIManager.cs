using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;

    [SerializeField]
    GameObject option;

    [SerializeField]
    GameObject computer;

    [SerializeField]
    GameObject phone;



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

    private void Update()
    {
        ChangeControllerUI();
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

    void ChangeControllerUI()
    {
        if(GameManager.instance.ct == GameManager.ControllType.Computer)
        {
            computer.SetActive(true);
            phone.SetActive(false);
        }
        else
        {
            computer.SetActive(false);
            phone.SetActive(true);
        }
    }

    public void EndUI()
    {

    }
}
