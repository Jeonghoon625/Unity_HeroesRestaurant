using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CharacterSelectController : MonoBehaviour
{
    public TextMeshProUGUI selectedHeroes;
    public TextMeshProUGUI combatAvr;
    public Image[] glowRound;
    public GameObject window;

    public HeroList heroList;

    public void OnEnable()
    {
        for(int i = 0; i < heroList.isSellect.Length; i++)
        {
            glowRound[i].gameObject.SetActive(heroList.isSellect[i]);
        }
    }

    public void ChangeSelectedHeroesNumber()
    {
        for (int i = 0; i < heroList.isSellect.Length; i++)
        {
            selectedHeroes.text = i +" / 4";
        }
    }

    public void OnAyranSelected()
    {
        glowRound[0].gameObject.SetActive(heroList.isSellect[0]);

    }
    public void OnCoqAuVinSelected()
    {
        glowRound[1].gameObject.SetActive(heroList.isSellect[1]);
    }
    public void OnFondueSelected()
    {
        glowRound[2].gameObject.SetActive(heroList.isSellect[2]);
    }
    public void OnLimuSelected()
    {
        glowRound[3].gameObject.SetActive(heroList.isSellect[3]);
    }
    public void OnHeroesSelected()
    {
        window.SetActive(false);
    }
    public void ShowCharacterSelectWindow()
    {
        window.SetActive(true);
    }
}
