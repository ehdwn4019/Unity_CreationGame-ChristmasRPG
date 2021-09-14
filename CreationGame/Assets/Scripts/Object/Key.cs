using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField]
    float speed = 5.0f;

    float y;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        y += speed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0f, y, 0f);
        
    }


}
