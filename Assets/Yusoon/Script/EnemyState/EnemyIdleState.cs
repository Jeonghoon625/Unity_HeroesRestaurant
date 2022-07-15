using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IEnemyState
{
    private Enemy enemy;
    public void IEnter(Enemy enemy)
    {
        this.enemy = enemy;
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
