using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//QuestText 풀링 
public class QuestProgressSpawn : MonoBehaviour
{
    public static QuestProgressSpawn instance = null;

    int count = 10;

    [SerializeField]
    GameObject responePos;

    //QuestName 풀링 
    public List<GameObject> namePooling = new List<GameObject>();
    
    //Quest 진행상황 풀링
    public List<GameObject> currentPooling = new List<GameObject>();

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    public void CreateText()
    {
        for(int i=0; i<10; i++)
        {
            GameObject questName = ObjectPoolingManager.Create("QuestName", responePos.transform.position + new Vector3(0f, 50.0f, 0f), Quaternion.identity);
            GameObject questCurrent = ObjectPoolingManager.Create("QuestCurrent", responePos.transform.position + new Vector3(0f, 20.0f, 0f), Quaternion.identity);

            //부모 오브젝트에 연결
            questName.transform.SetParent(responePos.transform);
            questCurrent.transform.SetParent(responePos.transform);

            questName.SetActive(false);
            questCurrent.SetActive(false);
            namePooling.Add(questName);
            currentPooling.Add(questCurrent);

        }
    }

    public void Disappear(GameObject questText , string name)
    {
        //이름에 따라서 풀링 구분
        if (name == "QuestName")
            ObjectPoolingManager.Put(questText, namePooling);
        else
            ObjectPoolingManager.Put(questText, namePooling);
    }

    public GameObject ApeearName(Vector3 startPos)
    {
        return ObjectPoolingManager.TakeOut(namePooling, startPos);
    }
    
    public GameObject AppearCurrent(Vector3 startPos)
    {
        return ObjectPoolingManager.TakeOut(currentPooling, startPos);
    }
}
