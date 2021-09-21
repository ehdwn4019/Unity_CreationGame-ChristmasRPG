using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asd : MonoBehaviour
{
    public int questId;
    public int questTalkIndex;

    Dictionary<int, QuestData> questList = new Dictionary<int, QuestData>();

    // Start is called before the first frame update
    void Start()
    {
        GenerateData();
    }

    void GenerateData()
    {
        //questList.Add(10, new QuestData("몬스터 퇴치", new int[] { 1001,1002}));
        //questList.Add(20, new QuestData("공주 구출 대작전", new int[] { 1002, 1003 }));
    }

    public int GetQuestTalkIndex(int id)
    {
        return questId + questTalkIndex;
    }

    //수락버튼 누르면 증가시키기 
    public string CheckQuest(int id)
    {
        //if(id == questList[questId].npcId[questTalkIndex])
        //    questTalkIndex++;
        //
        //if (questTalkIndex == questList[questId].npcId.Length)
        //    NextQuest();

        return questList[questId].questName;
    }

    void NextQuest()
    {
        questId += 10;
        questTalkIndex = 0;
    }

    //void ControlObject()
    //{
    //    switch(questId)
    //    {
    //        case 10:
    //
    //            if(questTalkIndex == 2)
    //                quest
    //
    //            break;
    //        case 20:
    //            break;
    //    }
    //}
}
