using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

//id = 1001

public class BoardQuest : MonoBehaviour , IPointerClickHandler
{
    [SerializeField]
    BoxCollider boxCollider;

    [SerializeField]
    GameObject questInfo;

    [SerializeField]
    Quest quest;
    
    Canvas canvas;
    QuestData npcData;

    [SerializeField]
    GameObject inGameCanvas;

    [SerializeField]
    GameObject questNameObj;

    [SerializeField]
    GameObject questCurrentObj;

    //[SerializeField]
    //Monster monster;

    public Action<int> monsterQuest; 

    Text questNameText;
    Text questCurrentText;

    public string questName = "몬스터 퇴치";
    //public string[] questInfo;
    //public string questReward;

    public int maxKillMonster = 5;
    public int currentKillMonster = 4;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponentInChildren<Canvas>();
        npcData = GetComponent<QuestData>();
        questNameText = questNameObj.GetComponent<Text>();
        questCurrentText = questCurrentObj.GetComponent<Text>();
        canvas.enabled = false;
        monsterQuest += SetQuestText;
        SetQuestInfo();
        //SetQuestText();
        questNameText.text = questName;
        questCurrentText.text = currentKillMonster + " /  " + maxKillMonster;
        //monster.monsterQuest += SetQuestText;
        //SetQuestText(1);
        //QuestProgressSpawn.instance.Appear(inGameCanvas.transform.position,name);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            boxCollider.enabled = true;
            canvas.enabled = true;

            if(IsTouchRewardBtn()== false && currentKillMonster >= maxKillMonster)
            {
                quest.SetQuestClear();
                //quest.SetQuest(gameObject , true);
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
            //if(questInfo != null)
            //{
            //    questInfo.SetActive(false);
            //}

            quest.talkIndex = 0;
            quest.acceptQuestBtn.interactable = false;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (boxCollider.enabled == true)
        {
            questInfo.SetActive(true);


            //if (questInfo != null)
            //{
            //    questInfo.SetActive(true);
            //}

        }
    }

    void SetQuestInfo()
    {
        npcData.questName = "몬스터 처치";
        QuestManger.instance.talkData.Add(npcData.id, new string[] 
        {
            "몬스터가 난동을 부립니다.","몬스터 좀 잡아주세요!","10마리면 될것같아요!"
        });
        npcData.questReward = "보상 : 1000원 , 열쇠 1개";
        //questInfo[1] = "몬스터 좀 잡아주세요";
        //questInfo[2] = "제발요";
        //questInfo[3] = "ㅠㅠ";
        //questReward = "1000원";
    }

    public void SetQuestText(int count = 1)
    {
        //Debug.Log("action call");
        float x = UnityEngine.Random.Range(-45f, 45f);
        currentKillMonster += count;
        GameObject goName = QuestProgressSpawn.instance.ApeearName(inGameCanvas.transform.position + new Vector3(x, 50.0f, 0f));
        GameObject goCurrent = QuestProgressSpawn.instance.AppearCurrent(inGameCanvas.transform.position + new Vector3(x, 20.0f, 0f));
        goName.GetComponent<Text>().text = questName;

        if (currentKillMonster < maxKillMonster)
            goCurrent.GetComponent<Text>().text = currentKillMonster + " /  " + maxKillMonster;
        else
        {
            goCurrent.GetComponent<Text>().text = "퀘스트 클리어!";
            quest.getRewardBtn.interactable = true;
            //GetReward();
        }

        //if(currentKillMonster < maxKillMonster)
        //{
        //    Debug.Log("action call");
        //    currentKillMonster += count;
        //    GameObject goName = QuestProgressSpawn.instance.ApeearName(inGameCanvas.transform.position + new Vector3(0f, 50.0f, 0f));
        //    GameObject goCurrent = QuestProgressSpawn.instance.AppearCurrent(inGameCanvas.transform.position + new Vector3(0f, 20.0f, 0f));
        //    goName.GetComponent<Text>().text = questName;
        //    goCurrent.GetComponent<Text>().text = currentKillMonster + " /  " + maxKillMonster; ;
        //}
        //else
        //{
        //    GameObject goName = QuestProgressSpawn.instance.ApeearName(inGameCanvas.transform.position + new Vector3(0f, 50.0f, 0f));
        //    goName.GetComponent<Text>().text = "퀘스트 클리어!";
        //}
        
    }

    public bool IsTouchRewardBtn()
    {
        return quest.getRewardBtn.interactable;
    }
}
