using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    private Heros hero;

    private float attackTimer;
    public void IEnter(Heros hero)
    {
        this.hero = hero;
        attackTimer = hero.AttackCool;
    }

    public void IUpdate()
    {

    }

    public void IFixedUpdate()
    {
        if(hero.target == null)
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
