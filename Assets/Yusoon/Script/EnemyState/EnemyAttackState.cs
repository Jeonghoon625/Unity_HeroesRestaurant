using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IEnemyState
{
    private Enemy enemy;
    private Heros hero;
    private float attackTimer;
    public void IEnter(Enemy enemy)
    {
        this.enemy = enemy;
        attackTimer = enemy.AttackCool;
        if(enemy.target != null)
        {
            hero = enemy.target.GetComponent<Heros>();
        }
    }

    public void IUpdate()
    {
    }

    public void IFixedUpdate()
    {
        if (enemy.target != null && Mathf.Abs(enemy.transform.position.x - enemy.target.transform.position.x) > enemy.attackAreaX + 0.5f)
        {
            enemy.target = null;
            enemy.SetState("Idle");
        }
        if (enemy.target == null || hero.hp == 0)
        {
            enemy.SetState("Idle");
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
