using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StunState : IState
{
    private Heros hero;

    private float timer;
    private float stunTime = 3f;
    private Vector3 dir = Vector3.zero;
    public void IEnter(Heros hero)
    {
        timer = 0f;
        this.hero = hero;
        hero.animator.SetBool("Stun", true);

        dir = hero.transform.position;
        if (hero.transform.position.x - hero.bossPos.x > 0)
        {
            dir.x += 2f;
        }
        else
        {
            dir.x -= 2f;
        }
        // 스턴 이펙트 생성
        hero.StartStun();
    }

    public void IUpdate()
    {
        hero.transform.position = Vector3.Lerp(hero.transform.position, dir, 10f * Time.deltaTime);
        timer += Time.deltaTime;
        if(timer > stunTime)
        {
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

        hero.skillButton.GetComponent<Button>().interactable = true;
    }
}
