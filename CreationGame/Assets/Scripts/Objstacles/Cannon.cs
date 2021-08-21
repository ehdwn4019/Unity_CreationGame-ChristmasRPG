using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : Obstacles
{
    [SerializeField]
    Transform target;

    protected override void Init()
    {
        base.Init();
    }

    protected override void Loop()
    {
        base.Loop();

        LookTarget();
    }

    void LookTarget()
    {
        Vector3 forward = target.position - transform.position;
        Quaternion quaternion = Quaternion.LookRotation(forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, quaternion, Time.deltaTime * 5.0f);
    }
}
