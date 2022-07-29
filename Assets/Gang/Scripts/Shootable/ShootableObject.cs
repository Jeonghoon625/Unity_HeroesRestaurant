using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DamageType
{
    CoqauVin,
    Fondue,
    Limu,
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
    private Vector3 hitPos = new Vector3(0f, 1f, 1f);

    private void Update()
    {
        dir.y = Mathf.Sin(Time.time * curveSpeed) * curveHeight;
        transform.position += dir * speed * Time.deltaTime;

        var cols = Physics.OverlapBox(transform.position, Vector3.zero);
        foreach (var col in cols)
        {
            if (col.transform.tag == "Monster")
            {
                GameObject hiteff;
                switch (damageType)
                {
                    case DamageType.CoqauVin:
                        //col.transform.GetComponent<Enemy>().OnHit(hero, hero.Dmg);
                        hiteff = MultipleObjectPooling.instance.GetPooledObject("HitEffect_N");
                        hiteff.SetActive(true);
                        hiteff.transform.position = col.transform.position + hitPos;
                        break;
                    case DamageType.Fondue:
                        //col.transform.GetComponent<Enemy>().OnHit(hero, hero.Dmg);
                        hiteff = MultipleObjectPooling.instance.GetPooledObject("HitEffect_L");
                        hiteff.SetActive(true);
                        hiteff.transform.position = col.transform.position + hitPos;
                        break;
                    case DamageType.Limu:
                        SplashDamage(col);
                        hiteff = MultipleObjectPooling.instance.GetPooledObject("HitEffect_W");
                        hiteff.SetActive(true);
                        hiteff.transform.position = col.transform.position + hitPos;
                        break;
                }
                col.transform.GetComponent<Enemy>().OnHit(hero, hero.Dmg);
                Destroy(gameObject);
                //Debug.Log(Vector3.up);
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
                if (col.gameObject == hit.gameObject)
                {
                    //col.transform.GetComponent<Enemy>().OnHit(hero, hero.Dmg);
                    continue;
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
