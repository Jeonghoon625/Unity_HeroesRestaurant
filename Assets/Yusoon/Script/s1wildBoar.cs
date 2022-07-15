using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System;

enum State
{
    Run,
    Attack,
    Dead,
}
public class s1wildBoar : MonoBehaviour
{
    private State state;
    public float monsterDamage = 1;

    private Transform target = null;
    private float moveSpeed = 2f;

    //public Slider sliderHp;
    public int maxHp = 100;
    private int currentHp;
    public int hp
    {
        get { return currentHp; }
        set 
        { 
            currentHp = value;
            //sliderHp.value = currentHp / maxHp;
        }
    }

    public Animator anim;

    private void Start()
    {
        currentHp = maxHp;
        //sliderHp.maxValue = maxHp;
        //sliderHp.value = currentHp;
    }

    private void Update()
    {
        if (state == State.Run)
        {
            UpdateRun();
        }
        else if (state == State.Attack)
        {
            UpdateAttack();
        }
        else if (state == State.Dead)
        {
            UpdateDead();
        }
    }

    private void UpdateRun()
    {
        if (target != null)
        {
            Vector3 dir = target.position - transform.position;
            transform.Translate(dir.normalized * moveSpeed * Time.deltaTime);
        }
        state = State.Run;
        anim.SetTrigger("Run");
    }

    private void UpdateAttack()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if(distance <= 2)
        {
            state = State.Attack;
            anim.SetTrigger("Attack");
        }

    }

    private void UpdateDead()
    {
        if(currentHp <= 0)
        {
            currentHp = 0;
            state = State.Dead;
            anim.SetTrigger("Dead");
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hero")
        {
            target = other.gameObject.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        target = null;
    }
}
