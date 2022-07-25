using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeroInfo : MonoBehaviour
{
    public GameObject[] heroesCard;

    public Image prevCard;
    public GameObject prevHeroCard;

    [SerializeField]
    private Reinforcement reinforcement;
    [SerializeField]
    private TextMeshProUGUI[] power;
    [SerializeField]
    private TextMeshProUGUI[] health;

    private void OnEnable()
    {
        var enhance = GameManager.Instance.goodsManager.enhance;
        foreach (var po in power)
        {
            po.text = $"{3 + Mathf.RoundToInt(3 * (reinforcement.power / 100f + enhance))}";
        }

        foreach (var heal in health)
        {
            heal.text = $"{100 + Mathf.RoundToInt(100 * (reinforcement.health / 100f + enhance))}";
        }
    }
    public void ShowHeroesInfo(Image select)
    {
        prevCard.color = Color.black;
        prevCard = select;
        select.color = Color.white;
    }
    public void ShowAyranInfo(int count)
    {
        prevHeroCard.SetActive(false);
        prevHeroCard = heroesCard[count];
        heroesCard[count].SetActive(true);
    }
}
