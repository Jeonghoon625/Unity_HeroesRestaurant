using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffTest : MonoBehaviour
{
    public bool playAura = true;
    public List<ParticleSystem> particleObjects = new List<ParticleSystem>();
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (playAura)
        //    particleObject.Play();
        //else if (!playAura)
        //    particleObject.Stop();

        if (Input.GetKeyDown(KeyCode.P))
        {
            ParticlePlay();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            ParticleStop();
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
