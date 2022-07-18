using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    private Heros hero;
    private Enemy enemy;
    private float attackTimer;
    public void IEnter(Heros hero)
    {
        this.hero = hero;
        attackTimer = hero.AttackCool;
        if (hero.target != null)
        {
            enemy = hero.target.GetComponent<Enemy>();
        }
    }

    public void IUpdate()
    {

    }

    public void IFixedUpdate()
    {
        if (hero.target == null || enemy.hp == 0)
        {
            hero.SetState("Idle");
        }

        attackTimer += Time.deltaTime;
        if (hero.AttackCool <= attackTimer)
        {
            attackTimer = 0f;
            hero.animator.SetTrigger("Attack");
        }
    }

    public void IExit()
    {
        hero.target = null;
    }
}
