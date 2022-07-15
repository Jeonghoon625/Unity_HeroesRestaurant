using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    private Unit unit;

    private float attackTimer;
    public void IEnter(Unit unit)
    {
        this.unit = unit;
        unit.animator.SetTrigger("Attack");
        attackTimer = 0f;
    }

    public void IUpdate()
    {
        
    }

    public void IFixedUpdate()
    {
        attackTimer += Time.deltaTime;
        if(unit.AttackCool <= attackTimer)
        {
            attackTimer = 0f;
            unit.animator.SetTrigger("Attack");
        }
    }

    public void IExit()
    {
        
    }
}
