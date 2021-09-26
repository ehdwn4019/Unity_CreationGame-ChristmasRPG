using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//퀘스트 이름 , 퀘스트 진행상황 텍스트   
public class QuestProgress : MonoBehaviour
{
    public Text text;
    Vector3 pos;
    Vector3 startPos;

    public float moveSpeed = 250.0f;
    public float destroyTime = 1.5f;
    string name;

    // Start is called before the first frame update
    void Start()
    {
        startPos = text.transform.position;
        name = this.gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {
        TextMove();
    }

    //퀘스트 텍스트 이동
    void TextMove()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);

        destroyTime -= Time.deltaTime;

        if (destroyTime <= 0)
        {
            QuestProgressSpawn.instance.Disappear(this.gameObject,name);
            destroyTime = 1.5f;
        }
    }
}
