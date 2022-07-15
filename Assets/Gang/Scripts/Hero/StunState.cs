using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunState : IState
{
    private Unit unit;
    public void IEnter(Unit unit)
    {
        this.unit = unit;
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
