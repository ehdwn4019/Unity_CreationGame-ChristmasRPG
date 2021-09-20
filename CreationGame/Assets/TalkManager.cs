using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData = new Dictionary<int, string[]>();

    // Start is called before the first frame update
    void Start()
    {

    }

    //void GenerateDate()
    //{
    //    talkData.Add()
    //}

    public string GetTalk(int id, int talkIndex)
    {
        return talkData[id][talkIndex];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
