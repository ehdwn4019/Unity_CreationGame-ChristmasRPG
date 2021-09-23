using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

//questManager
public class Quest : MonoBehaviour
{
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

    Action quest;

    public void Start()
    {
        acceptQuestBtn.interactable = false;
        getRewardBtn.gameObject.SetActive(false);

        // 프리팹 버튼에 버튼 이벤트 추가 
        talkIndexMinusBtn.onClick.AddListener(() => { talkIndex--; });
        talkIndexPlusBtn.onClick.AddListener(() => { talkIndex++; });
        acceptQuestBtn.onClick.AddListener(delegate { AcceptQuest(); });
        getRewardBtn.onClick.AddListener(delegate{ GetRewardKey(); });
        getRewardBtn.onClick.AddListener(delegate { GetRewardUseSled(); });
        getRewardBtn.onClick.AddListener(delegate { GetRewardClearGame(); });
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

        //int questTalkIndex = questManager.GetQuestTalkIndex(id);
        string talkData = QuestManger.instance.GetTalk(id, talkIndex);
        questInfoText.text = talkData;
    }

    public void AcceptQuest()
    {
        acceptQuestBtn.gameObject.SetActive(false);
        getRewardBtn.gameObject.SetActive(true);
        getRewardBtn.interactable = false;
    }

    void GetRewardKey()
    {
        inven.moneyChange.Invoke(5000);
        inven.GetRewardGrayKey(1);
        getRewardBtn.interactable = false;
        getRewardBtn.onClick.RemoveListener(delegate { GetRewardKey(); });
        //Destroy(questInfo);
    }

    void GetRewardUseSled()
    {
        getRewardBtn.interactable = false;
    }

    void GetRewardClearGame()
    {
        getRewardBtn.interactable = false;
    }

    //public bool IsTouchRewardBtn()
    //{
    //    return getRewardBtn.interactable;
    //}
}
