using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunState : IState
{
    private Heros hero;
    public void IEnter(Heros hero)
    {
        this.hero = hero;
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
