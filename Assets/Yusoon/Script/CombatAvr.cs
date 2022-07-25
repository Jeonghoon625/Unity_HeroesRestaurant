using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CombatAvr : MonoBehaviour
{
    public TextMeshProUGUI power1;
    public TextMeshProUGUI power2;
    public TextMeshProUGUI power3;
    public TextMeshProUGUI power4;

    public TextMeshProUGUI totalAverage;

    public void Update()
    {
        GetAverage();
    }
    public void GetAverage()
    {
        totalAverage.text = ((int.Parse(power1.text) + int.Parse(power2.text) + int.Parse(power3.text) + int.Parse(power4.text)) / 4).ToString();
    }
}
