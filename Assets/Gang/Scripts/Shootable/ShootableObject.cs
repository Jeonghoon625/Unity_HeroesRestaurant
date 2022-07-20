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
  
    private void Update()
    {
        dir.y = Mathf.Sin(Time.time *curveSpeed) * curveHeight;
        transform.position += dir * speed * Time.deltaTime;
        
        var cols = Physics.OverlapBox(transform.position, Vector3.zero);
        foreach (var col in cols)
        {
            if (col.transform.tag == "Monster")
            {
                switch(damageType)
                {
                    case DamageType.Basic:
                        col.transform.GetComponent<Enemy>().OnHit(hero, hero.Dmg);
                        break;
                    case DamageType.Splash:
                        SplashDamage(col);
                        break;
                }
                Destroy(gameObject);
            }
        }
    }

    private void SplashDamage(Collider col)
    {
        var splash = Physics.OverlapBox(transform.position, splashRange);
        foreach (var hit in splash)
        {
            if (hit.transform.tag == "Monster")
            {
                if(col.gameObject == hit.gameObject)
                {
                    col.transform.GetComponent<Enemy>().OnHit(hero, hero.Dmg);
                }
                else
                {
                    hit.transform.GetComponent<Enemy>().OnHit(hero, hero.Dmg * 0.1f);
                }
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
