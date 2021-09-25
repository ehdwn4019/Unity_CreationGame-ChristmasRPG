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

    QuestData data;

    public int talkIndex;

    
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

        // 버튼 사운드 추가
        talkIndexMinusBtn.onClick.AddListener(() => { SoundManager.instance.PlaySoundEffect("퀘스트버튼"); });
        talkIndexPlusBtn.onClick.AddListener(() => { SoundManager.instance.PlaySoundEffect("퀘스트버튼"); });
        acceptQuestBtn.onClick.AddListener(() => { SoundManager.instance.PlaySoundEffect("퀘스트버튼"); });
        getRewardBtn.onClick.AddListener(() => { SoundManager.instance.PlaySoundEffect("퀘스트버튼"); });
    }

    //퀘스트 클리어
    public void SetQuestClear()
    {
        questNameText.text = "";
        questInfoText.text = "";
        questRewardText.text = "";
        acceptQuestBtn.gameObject.SetActive(true);
        acceptQuestBtn.interactable = false;
        getRewardBtn.gameObject.SetActive(false);
        getRewardBtn.interactable = false;
    }

    //퀘스트 세팅
    public void SetQuest(GameObject npc)
    {
        QuestData npcData = npc.GetComponent<QuestData>();

        npcName = npc.name;

        questNameText.text = npcData.questName;
        questRewardText.text = npcData.questReward;
        SetTalk(npcData.id);
    }

    //퀘스트 내용 세팅
    void SetTalk(int id)
    {
        //페이지 넘기기 
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
