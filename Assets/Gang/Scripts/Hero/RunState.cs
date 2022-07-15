using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : IState
{
    private Heros hero;
    private Vector3 m_Position;
    private GameObject target;

    private Vector3 dir = new Vector3(0f, 0f, 0f);
    public void IEnter(Heros hero)
    {
        hero.animator.SetBool("Run", true);
        this.hero = hero;

        m_Position = hero.m_Position;
        target = hero.target;
        if (hero.transform.position.x - m_Position.x > 0)
        {
            dir.x = -1f;
            hero.transform.rotation = new Quaternion(0f, 180f, 0f, 0f);
        }
        else
        {
            dir.x = 1f;
            hero.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        }
    }

    public void IUpdate()
    {
        // ¿Ãµø
        var dir = hero.transform.position.x - m_Position.x;
        if (dir < 0.1f && dir > -0.1f)
        {
            hero.SetState("Idle");
        }
        else
        {
            hero.transform.position += this.dir * hero.runSpeed * Time.deltaTime;
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
        hero.animator.SetBool("Run", false);
    }

    private void OnTarget()
    {
        var cols = Physics.OverlapBox(hero.transform.position, hero.attackArea);
        foreach(var col in cols)
        {
            if(col.gameObject == target)
            {
                hero.SetState("Attack");
            }
        }
    }
}
