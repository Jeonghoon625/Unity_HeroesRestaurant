using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroInfo : MonoBehaviour
{
    public GameObject[] heroesCard;

    public Image prevCard;
    public GameObject prevHeroCard;
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
    //public void ShowCoQueVanInfo()
    //{
    //    prevHeroCard.SetActive(false);
    //    prevHeroCard = heroesCard[1];
    //    heroesCard[1].SetActive(true);
    //}
    //public void ShowFondueInfo()
    //{
    //    prevHeroCard.SetActive(false);
    //    prevHeroCard = heroesCard[1];
    //    heroesCard[1].SetActive(true);
    //}
    //public void ShowLimuInfo()
    //{
    //    prevHeroCard.SetActive(false);
    //    prevHeroCard = heroesCard[1];
    //    heroesCard[1].SetActive(true);
    //}
}
