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
    GameObject sledZone;

    [SerializeField]
    PlayerInventory inven;

    [SerializeField]
    Item grayKey;

    int needKey = 3;

    Canvas canvas;
    QuestData npcData;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponentInChildren<Canvas>();
        npcData = GetComponent<QuestData>();
        canvas.enabled = false;
        sledZone.SetActive(true);
        SetQuestInfo();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(npcData.id);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            boxCollider.enabled = true;
            canvas.enabled = true;

            if(QuestClear() && IsTouchRewardBtn() == false)
            {
                sledZone.SetActive(false);
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

            //if(questInfo != null)
            //{
            //    questInfo.SetActive(true);
            //}
        }
    }

    public void SetQuestInfo()
    {
        npcData.questName = "썰매 타기";
        QuestManger.instance.talkData.Add(npcData.id, new string[]
        {
            "썰매를 타고 싶나요?","썰매를 타면 스노우몬에게 갈수 있어요!","열쇠가 3개가 필요해요!"
        });
        npcData.questReward = "보상 : 썰매이용";
    }

    public bool QuestClear()
    {
        if(inven.items.ContainsKey(grayKey.itemName))
        {
            if (inven.items[grayKey.itemName] == needKey)
            {
                quest.getRewardBtn.interactable = true;
                return true;
            }
            else
            {
                return false;
            }
                
        }

        return false;
    }

    public bool IsTouchRewardBtn()
    {
        return quest.getRewardBtn.interactable;
    }

}
