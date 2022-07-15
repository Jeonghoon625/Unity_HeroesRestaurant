using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunState : IEnemyState
{
    private Enemy enemy;
    private Vector3 m_Position;
    private GameObject target;
    Vector3 dir = new Vector3(0f, 0f, 0f);
    public void IEnter(Enemy enemy)
    {
        enemy.animator.SetBool("Run", true);
        this.enemy = enemy;

        m_Position = enemy.m_Position;
        target = enemy.target;
        if (enemy.transform.position.x - m_Position.x > 0)
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
        // ¿Ãµø
        var dis = enemy.transform.position.x - m_Position.x;
        if (dis < 0.1f && dis > -0.1f)
        {
            enemy.SetState("Idle");
            enemy.animator.SetBool("Run", false);
        }
        else
        {
            enemy.transform.position += dir * enemy.runSpeed * Time.deltaTime;
        }


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

    }

    private void OnTarget()
    {
        var cols = Physics.OverlapBox(enemy.transform.position, enemy.attackArea);
        foreach (var col in cols)
        {
            if (col.CompareTag("Monster"))
            {
                enemy.SetState("Attack");
                enemy.animator.SetBool("Run", false);
            }
        }
    }
}
