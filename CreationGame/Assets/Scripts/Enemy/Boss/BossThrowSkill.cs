using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossThrowSkill : MonoBehaviour
{
    [SerializeField]
    Boss boss;

    [SerializeField]
    Animator animator;

    [SerializeField]
    GameObject target;

    [SerializeField]
    GameObject throwPos;

    float ballCount = 5;
    bool isThrowBall;

    public bool IsThrowBall { get { return isThrowBall; } }

    // Start is called before the first frame update
    void Start()
    {
        boss = GetComponent<Boss>();
    }

    // Update is called once per frame
    void Update()
    {
        Throw();
    }

    public void Throw()
    {
        if (boss.skillState != Boss.SkillState.Throw)
            return;

        boss.nav.ResetPath();
        animator.SetTrigger("Throw");
    }

    public void CreateBall()
    {
        for(int i=0; i<ballCount; i++)
        {
            GameObject go = ThrowBallSpawn.instance.Appear(throwPos.transform.position);
        }
    }

    public void ThrowBall()
    {
        isThrowBall = true;
    }

    public void End()
    {
        boss.skillState = Boss.SkillState.None;
        boss.state = Boss.EnemyState.Idle;
    }

}
