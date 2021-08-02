using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    protected enum ObstaclesType
    {
        None,
        FallingObj,
        Cannon,
    }

    ObstaclesType ot;

    // Start is called before the first frame update
    void Start()
    {
        ot = ObstaclesType.None;
        Init();
    }

    protected virtual void Init()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Loop();
    }

    protected virtual void Loop()
    {

    }

   

    

}
