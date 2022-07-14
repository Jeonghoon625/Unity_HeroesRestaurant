using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : IState
{
    private Unit unit;
    private Vector3 m_Position;
    private GameObject target;
    Vector3 dir = new Vector3(0f, 0f, 0f);
    public void IEnter(Unit unit)
    {
        unit.animator.SetBool("Run", true);
        this.unit = unit;

        m_Position = unit.m_Position;
        target = unit.target;
        if (unit.transform.position.x - m_Position.x > 0)
        {
            dir.x = -1f;
            unit.transform.rotation = new Quaternion(0f, 180f, 0f, 0f);
        }
        else
        {
            dir.x = 1f;
            unit.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        }
    }

    public void IUpdate()
    {
        // ¿Ãµø
        var dis = unit.transform.position.x - m_Position.x;
        if (dis < 0.1f && dis > -0.1f)
        {
            unit.SetState("Idle");
            unit.animator.SetBool("Run", false);
        }
        else
        {
            unit.transform.position += dir * unit.runSpeed * Time.deltaTime;
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
        var cols = Physics.OverlapBox(unit.transform.position, unit.attackArea);
        foreach(var col in cols)
        {
            if(col.CompareTag("Monster"))
            {
                unit.SetState("Attack");
                unit.animator.SetBool("Run", false);
            }
        }
    }
}
