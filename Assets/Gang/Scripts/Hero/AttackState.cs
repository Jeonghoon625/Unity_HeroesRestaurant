using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    private Unit unit;
    public void IEnter(Unit unit)
    {
        this.unit = unit;
        unit.animator.SetBool("Attack", true);
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
