using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollBall : Obstacles
{
    protected override void Init()
    {
        base.Init();
        ot = ObstaclesType.RollBall;
    }

    protected override void Loop()
    {
        base.Loop();
    }
}
