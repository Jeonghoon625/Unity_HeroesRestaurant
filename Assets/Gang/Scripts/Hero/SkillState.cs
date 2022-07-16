using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillState : IState
{
    private Heros hero;
    public void IEnter(Heros hero)
    {
        this.hero = hero;
        hero.animator.SetTrigger("Skill");
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
