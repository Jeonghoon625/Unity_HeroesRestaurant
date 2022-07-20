using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DamageType
{
    Basic,
    Splash,
}
public class ShootableObject : MonoBehaviour
{
    [SerializeField]
    private float speed = 20f;
    [SerializeField]
    private float curveSpeed = 20;
    [SerializeField]
    private float curveHeight = 2;
    [SerializeField]
    private DamageType damageType;

    private Heros hero;

    private float duration = 3f;
    private Vector3 dir = Vector3.zero;
    public Vector3 splashRange = Vector3.zero;
  
    private void FixedUpdate()
    {
        //transform.position += dir * speed * Time.deltaTime;
        dir.y = Mathf.Sin(Time.time *curveSpeed) * curveHeight;
        transform.position += dir * speed * Time.deltaTime;
        
        var cols = Physics.OverlapBox(transform.position, Vector3.zero);
        foreach (var col in cols)
        {
            if (col.transform.tag == "Monster")
            {
                // 데미지 줘야함
                var dmg = hero.Dmg;
                col.transform.GetComponent<Enemy>().OnHit(hero, dmg);

                if (damageType == DamageType.Splash)
                {
                    SplashDamage(col.gameObject);
                }

                Destroy(gameObject);
            }
        }
    }

    private void SplashDamage(GameObject col)
    {
        var splash = Physics.OverlapBox(transform.position, splashRange);
        foreach (var hit in splash)
        {
            if (col != hit.gameObject)
            {
                var splashDmg = hero.Dmg * 0.1f;
                hit.transform.GetComponent<Enemy>().OnHit(hero, splashDmg);
            }
        }
    }
    public void Set(Heros attacker, Vector3 pos, Quaternion rot)
    {
        hero = attacker;
        dir.x = rot.y == 0 ? -1f : 1f;
        transform.position = pos;
        transform.rotation = rot;
        Destroy(gameObject, duration);
    }
}
