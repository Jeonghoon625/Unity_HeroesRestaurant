using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Stage1Controller : MonoBehaviour
{
    public GameObject[] heroPrefab;
    public GameObject rewardPrefab;
    public GameObject firstRewardPrefab;
    public TextMeshProUGUI properCompatPower;
    public TextMeshProUGUI stage;
    public CharacterSelectController characterSelectController;
    public HeroList herolist;

    public void Update()
    {
        for (int i = 0; i < characterSelectController.heroList.isSellect.Length; i++)
        {
            if (characterSelectController.heroList.isSellect[i])
            {
                heroPrefab[i].SetActive(true);
            }

            if (!characterSelectController.heroList.isSellect[i])
            {
                heroPrefab[i].SetActive(false);
            }
        }   
    }
    public void stage1Btn()
    {
        stage.text = "1-1";
        properCompatPower.text = "3";
    }

    public void stage2Btn()
    {
        stage.text = "1-2";
        properCompatPower.text = "3";
    }

    public void StartStage1Btn()
    {
        var count = 0;
        foreach(var list in herolist.isSellect)
        {
            if(list == true)
            {
                count++;
            }
        }
        if(count == 0)
        {
            return;
        }

        GameManager.Instance.saveLoadManager.SaveTime();

        if (stage.text == "1-1")
        {
            GameManager.Instance.ChanageScene("Gang");
        }
        else if (stage.text == "1-2")
        {
            GameManager.Instance.ChanageScene("stage1-2");
        }
    }

    public void exitBtn()
    {
        gameObject.SetActive(false);
    }
}
