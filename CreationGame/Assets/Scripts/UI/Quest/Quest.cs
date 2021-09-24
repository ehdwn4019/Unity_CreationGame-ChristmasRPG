using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

//questManager
public class Quest : MonoBehaviour
{
    [SerializeField]
    GameObject sledZone;

    public Button talkIndexMinusBtn;
    public Button talkIndexPlusBtn;
    public Button acceptQuestBtn;

    public Button getRewardBtn;

    public Text questNameText;
    public Text questInfoText;
    public Text questRewardText;

    public PlayerInventory inven;
    public GameObject questInfo;

    

    public int talkIndex;

    QuestData data;
    string npcName;

    public void Start()
    {
        data = GetComponent<QuestData>();

        sledZone.SetActive(true);
        acceptQuestBtn.interactable = false;
        getRewardBtn.gameObject.SetActive(false);

        // 프리팹 버튼에 버튼 이벤트 추가 
        talkIndexMinusBtn.onClick.AddListener(() => { talkIndex--; });
        talkIndexPlusBtn.onClick.AddListener(() => { talkIndex++; });
        acceptQuestBtn.onClick.AddListener(delegate { AcceptQuest(); });
    }

    public void SetQuestClear()
    {
        questNameText.text = "";
        questInfoText.text = "";
        questRewardText.text = "";
        acceptQuestBtn.gameObject.SetActive(true);
        acceptQuestBtn.interactable = false;
        getRewardBtn.gameObject.SetActive(false);
        getRewardBtn.interactable = false;
        //AcceptQuest();
    }

    public void SetQuest(GameObject npc)
    {
        QuestData npcData = npc.GetComponent<QuestData>();

        npcName = npc.name;

        questNameText.text = npcData.questName;
        questRewardText.text = npcData.questReward;
        SetTalk(npcData.id);

        //if(isTalk == true)
        //{
        //    QuestData npcData = npc.GetComponent<QuestData>();
        //    questNameText.text = npcData.questName;
        //    questRewardText.text = npcData.questReward;
        //    SetTalk(npcData.id);
        //}
        //else
        //{
        //    QuestData npcData = npc.GetComponent<QuestData>();
        //    questNameText.text = npcData.questName;
        //    questRewardText.text = npcData.questReward;
        //}


        //if(isClear == true)
        //{
        //    questNameText.text = "";
        //    QuestData npcData = npc.GetComponent<QuestData>();
        //    //QuestManger.instance.talkData[npcData.id] = new string[] {"","",""};
        //    questInfoText.text = "";
        //    //QuestManger.instance.talkData.Remove(npcData.id);
        //    questRewardText.text = "";
        //}
        //else
        //{
        //    QuestData npcData = npc.GetComponent<QuestData>();
        //    questNameText.text = npcData.questName;
        //    questRewardText.text = npcData.questReward;
        //    SetTalk(npcData.id);
        //}
    }


    void SetTalk(int id)
    {
        if(talkIndex <= 0)
        {
            talkIndex = 0;
        }
        else if(talkIndex >= QuestManger.instance.ReturnTalkDataLength(id))
        {
            talkIndex = QuestManger.instance.ReturnTalkDataLength(id);
            acceptQuestBtn.interactable = true;
        }

        string talkData = QuestManger.instance.GetTalk(id, talkIndex);
        questInfoText.text = talkData;
    }

    //퀘스트 수락
    public void AcceptQuest()
    {
        acceptQuestBtn.gameObject.SetActive(false);
        getRewardBtn.gameObject.SetActive(true);
        getRewardBtn.interactable = false;
        SetReward();
    }

    public void SetReward()
    {
        //NPC 이름에 따라 버튼 이벤트 적용
        switch(npcName)
        {
            case "BoardQuest":
                getRewardBtn.onClick.RemoveAllListeners();
                getRewardBtn.onClick.AddListener(delegate { GetRewardKey(); });
                break;
            case "Santa":
                getRewardBtn.onClick.RemoveAllListeners();
                getRewardBtn.onClick.AddListener(delegate { GetRewardUseSled(); });
                break;
            case "Princess":
                getRewardBtn.onClick.RemoveAllListeners();
                getRewardBtn.onClick.AddListener(delegate { GetRewardClearGame(); });
                break;
        }
    }

    //게시판퀘스트 클리어
    void GetRewardKey()
    {
        inven.moneyChange.Invoke(5000);
        inven.GetRewardGrayKey(1);
        getRewardBtn.interactable = false;
        getRewardBtn.onClick.RemoveListener(delegate { GetRewardKey(); });
    }

    //산타퀘스트 클리어
    void GetRewardUseSled()
    {
        sledZone.SetActive(false);
        getRewardBtn.interactable = false;
    }

    //공주퀘스트 클리어
    void GetRewardClearGame()
    {
        getRewardBtn.interactable = false;
    }
}
