using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBall : MonoBehaviour
{
    Vector3 startPos;
    Quaternion startRotation;
    bool isThrowBall;
    float speed = 8.0f;

    private void Awake()
    {
        startPos = transform.position;
        startRotation = transform.rotation;
    }

    private void OnEnable()
    {
        transform.position = startPos;
        transform.rotation = startRotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            isThrowBall = false;
            Player player = other.GetComponent<Player>();
            player.DecreaseHP(30);
            ThrowBallSpawn.instance.Disappear(gameObject);
        }

        if(other.gameObject.name == "IceBallStopZone")
        {
            isThrowBall = false;
            ThrowBallSpawn.instance.Disappear(gameObject);
        }
    }
}
