using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sled : MonoBehaviour
{
    [SerializeField]
    float speed;

    bool booster;
    BoxCollider[] coll;

    // Start is called before the first frame update
    void Start()
    {
        booster = false;
        coll = GetComponents<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(booster)
            transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            booster = true;
        
            //for(int i=0; i<coll.Length; i++)
            //{
            //    coll[i].enabled = true;
            //}
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Stop")
        {
            Debug.Log("GG");
            booster = false;
        }
    }

    //private void OnCollisionExit(Collision collision)
    //{
    //    if(collision.gameObject.name =="Player")
    //    {
    //        for(int i=0; i<coll.Length; i++)
    //        {
    //            coll[i].enabled = false;
    //        }
    //    }
    //}
}
