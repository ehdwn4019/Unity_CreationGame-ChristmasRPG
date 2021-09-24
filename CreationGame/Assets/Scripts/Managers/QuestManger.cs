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
    } 
   
    public string GetTalk(int id, int talkIndex)
    {
        return talkData[id][talkIndex];
    }
    

    public int ReturnTalkDataLength(int id)
    {
        return talkData[id].Length-1;
    }
}
