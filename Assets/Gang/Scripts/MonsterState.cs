using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterState : MonoBehaviour
{
    public int hp = 100;
    private BoxCollider col;

    private void Awake()
    {
        col = GetComponent<BoxCollider>();
    }

    public int OnHit(Heros hero, int dmg)
    {
        hp -= dmg;

        if(hp <= 0)
        {
            hero.target = null;
            //hero.SetState("Idle");
            col.enabled = false;
            hp = 0;
            Destroy(gameObject, 1f);
        }

        return hp;
    }
}
