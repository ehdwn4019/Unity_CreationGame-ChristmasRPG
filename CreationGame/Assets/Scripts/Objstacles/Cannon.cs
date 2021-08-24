using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : Obstacles
{
    protected GameObject target;

    protected override void Init()
    {
        base.Init();
        ot = ObstaclesType.Cannon;
        target = GameObject.Find("Player");
    }

    protected override void Loop()
    {
        base.Loop();
        LookTarget();
    }

    void LookTarget()
    {
        Vector3 forward = target.transform.position - transform.position;
        Quaternion quaternion = Quaternion.LookRotation(forward);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, quaternion, 5.0f * Time.deltaTime).eulerAngles;
        transform.rotation = Quaternion.Euler(0f,rotation.y,0f);
    }
}
