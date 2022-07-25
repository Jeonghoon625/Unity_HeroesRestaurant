using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    private Heros hero;
    private Enemy enemy;
    private float attackTimer;

    private Vector3 m_Position;

    private Vector3 dir = Vector3.zero;
    private Quaternion rot = Quaternion.identity;
    private Quaternion hpRot = Quaternion.identity;
    public void IEnter(Heros hero)
    {
        this.hero = hero;
        attackTimer = hero.AttackCool;
        if (hero.target != null)
        {
            enemy = hero.target.GetComponent<Enemy>();
        }
        m_Position = hero.m_Position;
        if (hero.transform.position.x - m_Position.x > 0)
        {
            dir.x = -1f;
            rot.y = 180f;
        }
        else
        {
            dir.x = 1f;
            rot.y = 0f;
        }

        hero.transform.rotation = rot;
        hero.hpBar.rectTransform.rotation = hpRot;
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
        //hero.target = null;
    }
}
