using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunState : IState
{
    private Heros hero;

    private float timer;
    private float stunTime = 2f;

    private GameObject stun;
    public void IEnter(Heros hero)
    {
        this.hero = hero;
        hero.animator.SetBool("Stun", true);
        // 스턴 이펙트 생성
        hero.StartStun();
    }

    public void IUpdate()
    {
        timer += Time.deltaTime;
        if(timer > stunTime)
        {
            timer = 0;
            hero.SetState("Idle");
        }
    }

    public void IFixedUpdate()
    {

    }

    public void IExit()
    {
        hero.animator.SetBool("Stun", false);
        // 스턴 이펙트 삭제

        hero.EndStun();
    }
}
