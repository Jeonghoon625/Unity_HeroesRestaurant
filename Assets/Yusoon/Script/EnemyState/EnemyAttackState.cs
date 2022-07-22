using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IEnemyState
{
    private Enemy enemy;
    //private Heros hero;
    private float attackTimer;

    private Vector3 dir = Vector3.zero;
    private Quaternion rot = Quaternion.identity;

    private float timer;
    private const float delayTime = 0.5f;
    public void IEnter(Enemy enemy)
    {
        this.enemy = enemy;
        attackTimer = enemy.AttackCool;

        //if (enemy.target != null)
        //{
        //    hero = enemy.target.GetComponent<Heros>();
        //}
        //var target = enemy.target;

        //if (enemy.transform.position.x - target.transform.position.x > 0)
        //{
        //    dir.x = -1f;
        //    rot.y = 180f;
        //    //enemy.transform.rotation = new Quaternion(0f, 180f, 0f, 0f);
        //}
        //else
        //{
        //    dir.x = 1f;
        //    rot.y = 0f;
        //    //enemy.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        //}
        //enemy.transform.rotation = rot;
    }

    public void IUpdate()
    {
        
    }

    public void IFixedUpdate()
    {
        if (enemy.target != null)
        {
            if (Mathf.Abs(enemy.transform.position.x - enemy.target.transform.position.x) > enemy.attackAreaX)
            {
                enemy.target = null;
                enemy.SetState("Idle");
            }
        }
        else
        {
            timer += Time.deltaTime;
            if (timer > delayTime)
            {
                timer = 0f;
                enemy.SetState("Idle");
            }
        }

        attackTimer += Time.deltaTime;
        if (enemy.AttackCool <= attackTimer)
        {
            attackTimer = 0f;
            enemy.animator.SetTrigger("Attack");
        }
    }

    public void IExit()
    {
        enemy.target = null;
    }
}
