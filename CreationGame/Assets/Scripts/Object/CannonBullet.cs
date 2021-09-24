using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBullet : MonoBehaviour
{
    Rigidbody rigid;
    GameObject startPos;

    [SerializeField]
    float speed;

    float x;

    private void Awake()
    {
        startPos = GameObject.Find("ShootPos");
    }

    private void OnEnable()
    {
        rigid = gameObject.AddComponent<Rigidbody>();
        rigid = GetComponent<Rigidbody>();
        rigid.useGravity = false;
        rigid.drag = 10.0f;
        rigid.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
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
            float x = Random.Range(-1, 1);
            float z = 0;
    
            if (x == 0)
            {
                z = -1;
            }

            other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(x, 0f, z) * speed, ForceMode.Impulse);
        }
    
        if(other.gameObject.name == "CannonBulletStopZone"|| other.gameObject.name == "ResponeZone")
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
