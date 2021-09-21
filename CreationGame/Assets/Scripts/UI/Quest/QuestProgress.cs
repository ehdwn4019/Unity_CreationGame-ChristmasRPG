using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestProgress : MonoBehaviour
{
    public float moveSpeed;
    public float destroyTime;

    public Text text;

    Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TextMove();
    }

    void TextMove()
    {
        pos.Set(text.transform.position.x, text.transform.position.y + (moveSpeed * Time.deltaTime), text.transform.position.z);
        text.transform.position = pos;

        destroyTime -= Time.deltaTime;

        if (destroyTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
