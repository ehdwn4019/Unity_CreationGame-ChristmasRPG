using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public void Start()
    {
        acceptQuestBtn.interactable = false;
        getRewardBtn.gameObject.SetActive(false);

        // 프리팹 버튼에 버튼 이벤트 추가 
        talkIndexMinusBtn.onClick.AddListener(() => { talkIndex--; });
        talkIndexPlusBtn.onClick.AddListener(() => { talkIndex++; });
        acceptQuestBtn.onClick.AddListener(delegate { AcceptQuest(); });
        getRewardBtn.onClick.AddListener(delegate { GetReward(); });
    }

    public void SetQuest(GameObject npc)
    {
        QuestData npcData = npc.GetComponent<QuestData>();
        questNameText.text = npcData.questName;
        questRewardText.text = npcData.questReward;
        SetTalk(npcData.id);
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

    void AcceptQuest()
    {
        acceptQuestBtn.gameObject.SetActive(false);
        getRewardBtn.gameObject.SetActive(true);
        getRewardBtn.interactable = false;
    }

    void GetReward()
    {
        inven.moneyChange.Invoke(5000);
        inven.GetRewardGrayKey(1);
        Destroy(questInfo);
    }
}
