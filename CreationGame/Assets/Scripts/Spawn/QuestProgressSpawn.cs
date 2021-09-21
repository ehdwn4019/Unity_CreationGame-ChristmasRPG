using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestProgressSpawn : MonoBehaviour
{
    public static QuestProgressSpawn instance = null;

    int count = 10;

    [SerializeField]
    GameObject responePos;

    public List<GameObject> namePooling = new List<GameObject>();
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
            GameObject questCurrent = ObjectPoolingManager.Create("QuestCurrent", responePos.transform.position + new Vector3(0f, 30.0f, 0f), Quaternion.identity);
            questName.SetActive(false);
            questCurrent.SetActive(false);
            namePooling.Add(questName);
            currentPooling.Add(questCurrent);

        }
    }

    public void Disappear(GameObject name,GameObject current)
    {
        ObjectPoolingManager.Put(name, namePooling);
        ObjectPoolingManager.Put(current, currentPooling);
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
