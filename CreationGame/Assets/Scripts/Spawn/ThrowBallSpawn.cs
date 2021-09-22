using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBallSpawn : MonoBehaviour
{
    public static ThrowBallSpawn instance = null;

    [SerializeField]
    GameObject responePos;

    [SerializeField]
    GameObject throwBalls;

    public int ballCount = 15;

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
        for(int i=0; i<ballCount; i++)
        {
            GameObject ball = ObjectPoolingManager.Create("ThrowBall", responePos.transform.position, Quaternion.identity);
            ball.transform.SetParent(throwBalls.transform);
            ball.SetActive(false);
            ballPooling.Add(ball);
        }
        
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
