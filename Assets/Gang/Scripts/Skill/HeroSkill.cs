using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    Buff,
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
    private bool playAura = true;


    private void Start()
    {
        ParticlePlay();
    }
    private void Update()
    {
        if (playAura)
        {
            timer += Time.deltaTime;
            if(timer > duration)
            {
                ParticleStop();
                if(skillType == SkillType.Buff)
                {
                    var tmep = transform.parent.gameObject.GetComponent<Heros>();
                    tmep.isInvincibility = false;
                }
                Destroy(gameObject);
            }
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
