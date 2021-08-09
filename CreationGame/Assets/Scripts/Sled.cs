using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sled : MonoBehaviour
{
    bool booster;

    // Start is called before the first frame update
    void Start()
    {
        booster = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(booster)
            transform.Translate(Vector3.left * 5.0f * Time.deltaTime);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            booster = true;
        }
    }
}
