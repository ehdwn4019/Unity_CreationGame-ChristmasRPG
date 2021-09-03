using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBullet : MonoBehaviour
{
    [SerializeField]
    float speed;

    Rigidbody rigid;
    GameObject startPos;

    float x;

    private void Awake()
    {
        startPos = GameObject.Find("ShootPos");
    }

    private void OnEnable()
    {
        //if(rigid == null)
        rigid = gameObject.AddComponent<Rigidbody>();
        rigid = GetComponent<Rigidbody>();
        rigid.useGravity = false;
        rigid.drag = 10.0f;
        transform.rotation = startPos.transform.rotation;
    }

    private void FixedUpdate()
    {
        Shoot();
    }

    void Shoot()
    {
        rigid.AddForce(Vector3.left * speed, ForceMode.Impulse);
        x += 360.0f * 5.0f* Time.fixedDeltaTime;
        rigid.MoveRotation(Quaternion.Euler(x, 0f, 0f));

    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            //other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.back * bulletSpeed, ForceMode.Impulse);
            float x = Random.Range(-1, 1);
            float z = 0;

            if (x == 0)
                z = -1;
            //other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(x, 0f, z) * speed, ForceMode.Impulse);
            CannonBulletSpawn.instance.Disappear(gameObject);
            Destroy(rigid);
            Invoke("ReturnPos", 2.0f);
        }

        if(other.gameObject.name == "CannonBulletStopZone"|| other.gameObject.name == "RightMapZone")
        {
            CannonBulletSpawn.instance.Disappear(gameObject);
            Destroy(rigid);
            Invoke("ReturnPos", 2.0f);
        }
    }
    
    void ReturnPos()
    {
        
        CannonBulletSpawn.instance.Appear(startPos.transform.position);
    }
}
