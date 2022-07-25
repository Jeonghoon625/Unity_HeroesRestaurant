using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoneState : IState
{
    private Heros hero;
    public void IEnter(Heros hero)
    {
        hero.animator.SetBool("Run", false);
        hero.animator.SetBool("Stun", false);
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
