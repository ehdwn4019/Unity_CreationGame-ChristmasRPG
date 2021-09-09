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
        if ((boss.skillState != Boss.SkillState.Throw) && (boss.state == Enemy.EnemyState.None))
            return;

        transform.forward = target.transform.position - transform.position;
        animator.SetTrigger("Throw");
    }

    public void CreateBall()
    {

    }


}
