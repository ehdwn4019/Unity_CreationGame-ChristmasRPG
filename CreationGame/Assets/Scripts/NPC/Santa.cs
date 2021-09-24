using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Santa : MonoBehaviour , IPointerClickHandler
{
    [SerializeField]
    BoxCollider boxCollider;

    [SerializeField]
    GameObject questInfo;

    [SerializeField]
    Quest quest;

    [SerializeField]
    PlayerInventory inven;

    [SerializeField]
    Item grayKey;

    Canvas canvas;
    QuestData npcData;

    int needKey = 3;
    bool isClearQuest;

    public bool IsClearQuest { get { return isClearQuest; } }

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
                //sledZone.SetActive(false);
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
        if(other.gameObject.name == "Player")
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
        npcData.questName = "썰매 타기";

        if(!QuestManger.instance.talkData.ContainsKey(npcData.id))
        {
            QuestManger.instance.talkData.Add(npcData.id, new string[]
            {
                "썰매를 타고 공주를 구해주세요!","썰매를 타면 스노우몬에게 갈수 있어요!","썰매를 타려면 열쇠가 3개가 필요해요!","열쇠는 퀘스트를 완료하고 장애물을 통과하면 얻을 수 있어요!"
            });
        }
        
        npcData.questReward = "보상 : 썰매이용";
    }

    //퀘스트 클리어 조건 
    public void QuestClear()
    {
        if (isClearQuest)
            return;

        if(inven.items.ContainsKey(grayKey.itemName))
        {
            if (inven.items[grayKey.itemName] >= needKey)
            {
                quest.getRewardBtn.interactable = true;
                isClearQuest = true;
            }
        }
    }

    public bool IsTouchRewardBtn()
    {
        return quest.getRewardBtn.interactable;
    }

}
