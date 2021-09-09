using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBallSpawn : MonoBehaviour
{
    public static ThrowBallSpawn instance = null;

    [SerializeField]
    GameManager responePos;

    List<GameObject> ballPooling = new List<GameObject>();

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    public void CreateBall()
    {
        GameObject ball = ObjectPoolingManager.Create("ThrowBall", responePos.transform.position, Quaternion.identity);
        ballPooling.Add(ball);
    }

    public void Disappear(GameObject ball)
    {
        ObjectPoolingManager.Put(ball, ballPooling);
    }

    public GameObject Appear(Vector3 startPos)
    {
        return ObjectPoolingManager.TakeOut(ballPooling, startPos);
    }

}
