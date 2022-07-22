using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunState : IEnemyState
{
    private Enemy enemy;
    private Heros hero;
    private GameObject target;

    private Vector3 dir = Vector3.zero;
    public void IEnter(Enemy enemy)
    {
        enemy.animator.SetBool("Run", true);
        this.enemy = enemy;

        target = enemy.target;
        hero = target.GetComponent<Heros>();
        if (enemy.transform.position.x - target.transform.position.x > 0)
        {
            dir.x = -1f;
            enemy.transform.rotation = new Quaternion(0f, 180f, 0f, 0f);
        }
        else
        {
            dir.x = 1f;
            enemy.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        }
    }

    public void IUpdate()
    {
        if(hero.hp == 0)
        {
            enemy.target = null;
            enemy.SetState("Idle");
        }
        // ¿Ãµø
        enemy.transform.position += dir * enemy.runSpeed * Time.deltaTime * enemy.speedDebuff;

        if (target != null)
        {
            OnTarget();
        }
    }

    public void IFixedUpdate()
    {
    }

    public void IExit()
    {
        enemy.animator.SetBool("Run", false);
    }

    private void OnTarget()
    {
        var cols = Physics.OverlapBox(enemy.transform.position, enemy.attackArea);
        foreach (var col in cols)
        {
            if (col.transform.tag == "Hero")
            {
                enemy.SetState("Attack");
            }
        }
    }
}
