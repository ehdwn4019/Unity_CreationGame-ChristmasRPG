using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //싱글톤 
    public static GameManager instance = null;

    Player player;

    public enum ControllType
    {
        Computer,
        Phone,
    }

    public ControllType ct = ControllType.Computer;

    public bool isFightBoss;

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
    }

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();

        //미리 생성 
        MonsterSpawn.instance.CreateMonster();
        FallBlockSpawn.instance.CreateBlock();
        RollBallSpawn.instance.CreateBall();
        CannonBulletSpawn.instance.CreateBullet();
        ThrowBallSpawn.instance.CreateBall();
        QuestProgressSpawn.instance.CreateText();
    }

    public void ControllerChange()
    {
        switch(ct)
        {
            case ControllType.Computer:
                ct = ControllType.Phone;
                UIManager.instance.ChangeControllerUI();
                break;
    
            case ControllType.Phone:
                ct = ControllType.Computer;
                UIManager.instance.ChangeControllerUI();
                break;
        }
    }
}
