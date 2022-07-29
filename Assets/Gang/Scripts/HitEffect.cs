using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    private float timer = 0f;
    private float liveTime = .5f;

    private void OnEnable()
    {
        timer = 0f;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > liveTime)
        {
            gameObject.SetActive(false);
        }
    }
}
