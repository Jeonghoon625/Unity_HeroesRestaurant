using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : ShootableObject
{
    Vector3 dir = Vector3.zero;

    private void Awake()
    {
        Destroy(gameObject, duration);
    }
    private void Update()
    {
        dir.x = transform.rotation.y == 0 ? -1f : 1f;
        transform.position += dir * speed * Time.deltaTime;

        var cols = Physics.OverlapBox(transform.position, Vector3.zero);
        foreach (var col in cols)
        {
            if (col.transform.tag == "Monster" && col != null)
            {
                // 데미지 줘야함
                var heros = hero.GetComponent<Heros>();
                var dmg = heros.Dmg;
                col.transform.GetComponent<MonsterState>().OnHit(heros, dmg);

                Destroy(gameObject);
            }
        }
    }
}
