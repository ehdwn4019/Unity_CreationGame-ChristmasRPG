using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotate : MonoBehaviour
{
    [SerializeField]
    float speed = 5.0f;

    float y;

    // Update is called once per frame
    void Update()
    {
        y += speed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0f, y, 0f);
        
    }
}
