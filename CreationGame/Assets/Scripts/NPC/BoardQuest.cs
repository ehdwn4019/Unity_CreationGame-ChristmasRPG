using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class BoardQuest : MonoBehaviour , IPointerClickHandler
{
    [SerializeField]
    BoxCollider boxCollider;

    [SerializeField]
    Quest quest;
    
    Canvas canvas;
    QuestData npcData;

    [SerializeField]
    GameObject questInfo;

    [SerializeField]
    GameObject inGameCanvas;

    [SerializeField]
    GameObject questNameObj;

    [SerializeField]
    GameObject questCurrentObj;

    //몬스터 퀘스트 진행 액션
    public Action<int> monsterQuest; 

    Text questNameText;
    Text questCurrentText;

    [SerializeField]
    string questName = "몬스터 퇴치";

    [SerializeField]
    int maxKillMonster = 5;

    [SerializeField]
    int currentKillMonster = 4;

    bool isClearQuest;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponentInChildren<Canvas>();
        npcData = GetComponent<QuestData>();
        questNameText = questNameObj.GetComponent<Text>();
        questCurrentText = questCurrentObj.GetComponent<Text>();
        canvas.enabled = false;
        isClearQuest = false;
        monsterQuest += SetQuestText;
        SetQuestInfo();
        questNameText.text = questName;
        questCurrentText.text = currentKillMonster + " /  " + maxKillMonster;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            boxCollider.enabled = true;
            canvas.enabled = true;

            if(isClearQuest && IsTouchRewardBtn()== false)
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
            quest.acceptQuestBtn.interactable = false;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (boxCollider.enabled == true)
        {
            questInfo.SetActive(true);
        }
    }

    //퀘스트 대화정보 셋팅
    void SetQuestInfo()
    {
        npcData.questName = "몬스터 처치";

        if (!QuestManger.instance.talkData.ContainsKey(npcData.id))
        {
            QuestManger.instance.talkData.Add(npcData.id, new string[]
            {
                "몬스터가 난동을 부립니다.","몬스터 좀 잡아주세요!","5마리면 될것같아요!"
            });
        }

        npcData.questReward = "보상 : 5000원 , 열쇠 1개";
    }

    //퀘스트 클리어 조건 및 퀘스트 진행상황 텍스트
    public void SetQuestText(int count = 1)
    {
        float x = UnityEngine.Random.Range(-45f, 45f);
        currentKillMonster += count;
        GameObject goName = QuestProgressSpawn.instance.ApeearName(inGameCanvas.transform.position + new Vector3(x, 50.0f, 0f));
        GameObject goCurrent = QuestProgressSpawn.instance.AppearCurrent(inGameCanvas.transform.position + new Vector3(x, 20.0f, 0f));
        goName.GetComponent<Text>().text = questName;

        if (currentKillMonster < maxKillMonster)
        {
            goCurrent.GetComponent<Text>().text = currentKillMonster + " /  " + maxKillMonster;
        }
        else
        {
            goCurrent.GetComponent<Text>().text = "퀘스트 클리어!";
            isClearQuest = true;
            quest.getRewardBtn.interactable = true;
        }
    }

    public bool IsTouchRewardBtn()
    {
        return quest.getRewardBtn.interactable;
    }
}
