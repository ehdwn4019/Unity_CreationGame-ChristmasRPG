using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    Camera cam;

    // Start is called before the first frame 

    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.rotation = Quaternion.Euler(cam.transform.rotation.x, cam.transform.rotation.y+90 , cam.transform.rotation.z);
        transform.LookAt(cam.transform.position);
    }
}
