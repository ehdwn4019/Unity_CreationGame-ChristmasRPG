using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Princess : MonoBehaviour , IPointerClickHandler
{
    [SerializeField]
    BoxCollider boxCollider;

    [SerializeField]
    GameObject questInfo;

    [SerializeField]
    Quest quest;

    [SerializeField]
    Boss boss;

    [SerializeField]
    GameObject returnPos;

    Canvas canvas;
    QuestData npcData;

    bool isClearQuest;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponentInChildren<Canvas>();
        npcData = GetComponent<QuestData>();
        canvas.enabled = false;
        isClearQuest = false;
        SetQuestInfo();
    }

    private void OnTriggerEnter(Collider other)
    {
        QuestClear();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            boxCollider.enabled = true;
            canvas.enabled = true;

            if(isClearQuest && IsTouchRewardBtn() == false)
            {
                quest.SetQuestClear();
            }
            else
            {
                quest.SetQuest(gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            boxCollider.enabled = false;
            canvas.enabled = false;
            questInfo.SetActive(false);
            quest.talkIndex = 0;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(boxCollider.enabled == true)
        {
            questInfo.SetActive(true);
        }
    }

    //퀘스트 대화정보 셋팅 
    public void SetQuestInfo()
    {
        npcData.questName = "공주 구출";

        if (!QuestManger.instance.talkData.ContainsKey(npcData.id))
        {
            QuestManger.instance.talkData.Add(npcData.id, new string[]
            {
                "구해주셔서 감사해요!","저와 함께 마을로 돌아가요!"
            });
        }
       
        npcData.questReward = "보상 : 게임클리어";
    }

    //퀘스트 클리어 조건
    private void QuestClear()
    {
        if (isClearQuest)
            return;

        if(boss.IsDie && transform.position == returnPos.transform.position)
        {
            quest.getRewardBtn.interactable = true;
            isClearQuest = true;
            QuestManger.instance.talkData[npcData.id] = new string[] { "감사합니다!" };
        }
    }

    public bool IsTouchRewardBtn()
    {
        return quest.getRewardBtn.interactable;
    }

    
}
