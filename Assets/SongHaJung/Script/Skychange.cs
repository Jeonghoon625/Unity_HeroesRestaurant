using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skychange : MonoBehaviour
{
    public Color colorStart = Color.blue;
    public Color colorEnd = Color.red;
    public float duration = 1.0F;

    private void Update()
    {
        float lerp = Mathf.PingPong(Time.time, duration) / duration;
        RenderSettings.skybox.SetColor("_Tint", Color.Lerp(colorStart, colorEnd, lerp));
    }
}
