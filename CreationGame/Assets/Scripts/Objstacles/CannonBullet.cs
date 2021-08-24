using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBullet : Cannon
{
    [SerializeField]
    float bulletPower;

    GameObject testTarget;

    Rigidbody rigid;
    GameObject startPos;

    bool isShoot;

    float tempAngle = 90.0f;

    private void Awake()
    {
        startPos = GameObject.Find("ShootPos");
        testTarget = GameObject.Find("TestTarget");
    }

    private void OnEnable()
    {
        rigid = GetComponent<Rigidbody>();
        transform.localRotation = startPos.transform.rotation;
    }

    protected override void Init()
    {
        base.Init();
    }

    protected override void FixedLoop()
    {
        base.FixedLoop();
    
        if(Input.GetKey(KeyCode.Q))
        Shoot();
    }
    
    void Shoot()
    {
        //Vector3 forward = target.transform.position - transform.position;
        rigid.AddForce(Vector3.left * bulletPower, ForceMode.Impulse);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name =="Player")
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.back * bulletPower, ForceMode.Impulse);
            CannonBulletSpawn.instance.Disappear(gameObject);
            Invoke("ReturnPos", 2.0f);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "RightMapZone")
        {
            CannonBulletSpawn.instance.Disappear(gameObject);
            Destroy(rigid);
            Invoke("ReturnPos", 2.0f);
            
        }
    }
    
    void ReturnPos()
    {
        //transform.position = startPos;
        gameObject.AddComponent<Rigidbody>();
        Vector3 velocity = GetVelocity(transform.position, testTarget.transform.position, tempAngle);
        rigid.velocity = velocity;
        CannonBulletSpawn.instance.Appear(startPos.transform.position);
    }

    public Vector3 GetVelocity(Vector3 player, Vector3 target, float initialAngle)
    {
        float gravity = Physics.gravity.magnitude;
        float angle = initialAngle * Mathf.Deg2Rad;

        Vector3 planarTarget = new Vector3(target.x, 0, target.z);
        Vector3 planarPosition = new Vector3(player.x, 0, player.z);

        float distance = Vector3.Distance(planarTarget, planarPosition);
        float yOffset = player.y - target.y;

        float initialVelocity
            = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));

        Vector3 velocity
            = new Vector3(0f, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));

        float angleBetweenObjects
            = Vector3.Angle(Vector3.forward, planarTarget - planarPosition) * (target.x > player.x ? 1 : -1);
        Vector3 finalVelocity
            = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * velocity;

        return finalVelocity;
    }
}
