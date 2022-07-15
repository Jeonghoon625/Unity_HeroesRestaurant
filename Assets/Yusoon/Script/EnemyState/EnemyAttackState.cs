using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IEnemyState
{
    private Enemy enemy;
    public void IEnter(Enemy enemy)
    {
        this.enemy = enemy;
        enemy.animator.SetBool("Attack", true);
    }

    public void IUpdate()
    {

    }

    public void IFixedUpdate()
    {

    }

    public void IExit()
    {

    }
}
