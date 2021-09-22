using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestProgress : MonoBehaviour
{
    public float moveSpeed = 250.0f;
    public float destroyTime = 1.5f;

    public Text text;

    Vector3 pos;
    Vector3 startPos;


    string name;

    //private void OnEnable()
    //{
    //    text.transform.position = startPos;
    //}

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

    void TextMove()
    {
        //text.transform.position = new Vector3(text.transform.position.x, text.transform.position.y + (moveSpeed * Time.deltaTime), text.transform.position.z);
        //text.transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        //transform.position += new Vector3(transform.position.x,transform.position.y * moveSpeed * Time.deltaTime , transform.position.z);
        //pos.Set(text.transform.position.x, text.transform.position.y + (moveSpeed * Time.deltaTime), text.transform.position.z);
        //text.transform.position = pos;

        destroyTime -= Time.deltaTime;

        if (destroyTime <= 0)
        {
            QuestProgressSpawn.instance.Disappear(this.gameObject,name);
            destroyTime = 1.5f;
        }
    }
}
