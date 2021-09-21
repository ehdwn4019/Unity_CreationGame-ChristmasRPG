using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManger : MonoBehaviour
{
    public static QuestManger instance = null;

    public Dictionary<int, string[]> talkData = new Dictionary<int, string[]>();

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    //// Start is called before the first frame update
    //void Start()
    //{
    //    GenerateData();
    //}
    //
    //void GenerateData()
    //{
    //    talkData.Add(1001, new string[] 
    //    { "몬스터가 난동을 부립니다.","몬스터 10마리 잡아주세요","제발요","Please","hey","Guy?,","HolyMoly" });
    //    talkData.Add(1002, new string[] { "열쇠 3개 가져오기" });
    //    talkData.Add(1003, new string[] { "구해주셔서 감사합니다." });
    //}
    
    public string GetTalk(int id, int talkIndex)
    {
        return talkData[id][talkIndex];
    }
    
    public int ReturnTalkDataLength(int id)
    {
        return talkData[id].Length-1;
    }
}
