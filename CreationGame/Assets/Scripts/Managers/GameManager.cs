using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    //싱글톤
    public static GameManager instance = null;

    //[SerializeField] Monster monster;
    //[SerializeField] Boss boss;
    //[SerializeField] Player player;

    Action playerDie;
    
    public enum ControllType
    {
        Computer,
        Phone,
    }

    public ControllType ct = ControllType.Computer;

    public bool isFightBoss;

    #region 프로퍼티들

    //public Monster Monster
    //{
    //    get
    //    {
    //        return monster;
    //    }
    //}
    //public Boss Boss
    //{
    //    get
    //    {
    //        return boss;
    //    }
    //}
    //public Player Player
    //{
    //    get
    //    {
    //        return player;
    //    }
    //}

    #endregion

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

    // Start is called before the first frame update
    void Start()
    {
        MonsterSpawn.instance.CreateMonster();
        FallBlockSpawn.instance.CreateBlock();
        RollBallSpawn.instance.CreateBall();
        CannonBulletSpawn.instance.CreateBullet();
        ThrowBallSpawn.instance.CreateBall();

        //playerDie.Invoke();
    }

    //public void ChangeComputer()
    //{
    //    ct = ControllType.Phone;
    //    computer.SetActive(false);
    //    phone.SetActive(true);
    //}
    //
    //public void ChangePhone()
    //{
    //    ct = ControllType.Computer;
    //    computer.SetActive(true);
    //    phone.SetActive(false);
    //}

    public void ControllerChange()
    {
        switch(ct)
        {
            case ControllType.Computer:
                ct = ControllType.Phone;
                break;
    
            case ControllType.Phone:
                ct = ControllType.Computer;
                break;
        }
    }

    void EndGame()
    {
        //if(player.IsDie)
        //{
        //    //SceneManager.LoadScene("TEST");
        //    SceneController.instance.GoStartScene();
        //}
    }
}
