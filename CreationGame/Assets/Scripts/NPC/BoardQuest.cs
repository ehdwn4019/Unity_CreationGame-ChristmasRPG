using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//id = 1001

public class BoardQuest : MonoBehaviour , IPointerClickHandler
{
    [SerializeField]
    BoxCollider boxCollider;

    [SerializeField]
    GameObject questBoard;

    [SerializeField]
    Quest quest;

    QuestData npcData;
    Canvas canvas;

    public string questName;
    public string[] questInfo;
    public string questReward;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponentInChildren<Canvas>();
        npcData = GetComponent<QuestData>();
        canvas.enabled = false;
        SetQuestInfo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            boxCollider.enabled = true;
            canvas.enabled = true;
            quest.SetQuest(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            boxCollider.enabled = false;
            canvas.enabled = false;
            questBoard.SetActive(false);
            quest.talkIndex = 0;
            quest.acceptQuestBtn.interactable = false;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (boxCollider.enabled == true)
        {
            questBoard.SetActive(true);
        }
    }

    void SetQuestInfo()
    {
        npcData.questName = "몬스터 처치";
        QuestManger.instance.talkData.Add(npcData.id, new string[] {
            "몬스터가 난동을 부립니다.","몬스터 좀 잡아주세요","10마리면 될것같아요"
        });
        npcData.questReward = "보상 : 1000원 , 열쇠 1개";
        //questInfo[1] = "몬스터 좀 잡아주세요";
        //questInfo[2] = "제발요";
        //questInfo[3] = "ㅠㅠ";
        //questReward = "1000원";
    }
}
