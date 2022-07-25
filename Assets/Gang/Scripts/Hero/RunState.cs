using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : IState
{
    private Heros hero;
    private Enemy enemy;
    private Vector3 m_Position;
    private GameObject target;

    private Vector3 dir = Vector3.zero;
    private Quaternion rot = Quaternion.identity;
    private Quaternion hpRot = Quaternion.identity;
    public void IEnter(Heros hero)
    {
        hero.animator.SetBool("Run", true);
        this.hero = hero;


        m_Position = hero.m_Position;
        target = hero.target;
        if (hero.target != null)
        {
            enemy = target.GetComponent<Enemy>();
        }
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
        if (target != null)
        {
            OnTarget();
            if (enemy.hp == 0)
            {
                hero.target = null;
                hero.SetState("Idle");
            }
        }
        // �̵�
        var dist = hero.transform.position.x - m_Position.x;
        if (dist < 0.1f && dist > -0.1f)
        {
            hero.SetState("Idle");
        }
        else
        {
            hero.transform.position += this.dir * hero.runSpeed * Time.deltaTime;
        }
    }

    public void IFixedUpdate()
    {

    }

    public void IExit()
    {
        hero.animator.SetBool("Run", false);
    }

    private void OnTarget()
    {
        var cols = Physics.OverlapBox(hero.transform.position, hero.attackArea);
        foreach (var col in cols)
        {
            if (col.gameObject == target)
            {
                hero.SetState("Attack");
            }
        }
    }
}
