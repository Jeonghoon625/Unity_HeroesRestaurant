using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SellcetHeroReinforce : MonoBehaviour
{
    [SerializeField]
    private Reinforcement reinforcement;
    [SerializeField]
    private TextMeshProUGUI power;

    private void OnEnable()
    {
        var enhance = GameManager.Instance.goodsManager.enhance;
        power.text = $"{3 + Mathf.RoundToInt(3 * (reinforcement.power / 100f + enhance))}";
    }
}
