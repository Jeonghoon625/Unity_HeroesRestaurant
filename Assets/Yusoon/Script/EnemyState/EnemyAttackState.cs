using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IEnemyState
{
    private Enemy enemy;
    private float attackTimer;
    public void IEnter(Enemy enemy)
    {
        this.enemy = enemy;
        attackTimer = enemy.AttackCool;
    }

    public void IUpdate()
    {
    }

    public void IFixedUpdate()
    {
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
