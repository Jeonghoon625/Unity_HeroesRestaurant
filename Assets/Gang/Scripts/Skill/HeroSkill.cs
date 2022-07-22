using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    Invincibilit,
    Shield,
    Attack,
}
public class HeroSkill : MonoBehaviour
{
    [SerializeField]
    private SkillType skillType;
    [SerializeField]
    private List<ParticleSystem> particleObjects = new List<ParticleSystem>();
    [SerializeField]
    private float duration;
    private float timer;


    private void Start()
    {
        ParticlePlay();
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > duration)
        {
            ParticleStop();

            if(skillType != SkillType.Attack)
            {
                var hero = transform.parent.gameObject.GetComponent<Heros>();
                if (skillType == SkillType.Invincibilit)
                {
                    hero.isInvincibility = false;
                }
                else if (skillType == SkillType.Shield)
                {
                    hero.isShield = false;
                    hero.hpBar.GetComponent<HpBar>().OffShield();
                }
            }

            Destroy(gameObject);
        }
    }
    private void ParticlePlay()
    {
        foreach (ParticleSystem particle in particleObjects)
        {
            particle.Play();
        }
    }
    private void ParticleStop()
    {
        foreach (ParticleSystem particle in particleObjects)
        {
            particle.Stop();
        }
    }
}
