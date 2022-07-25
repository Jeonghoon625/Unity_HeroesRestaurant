using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Stage2Controller : MonoBehaviour
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
        for(int i = 0; i < characterSelectController.heroList.isSellect.Length; i++)
        {
            if (characterSelectController.heroList.isSellect[i])
            {
                heroPrefab[i].SetActive(true);
            }

            if(!characterSelectController.heroList.isSellect[i])
            {
                heroPrefab[i].SetActive(false);
            }
        }
    }

    public void stage1Btn()
    {
        stage.text = "2-1";
        properCompatPower.text = "4";
    }

    public void stage2Btn()
    {
        stage.text = "2-2";
        properCompatPower.text = "5";
    }

    public void StartStageBtn()
    {
        var count = 0;
        foreach (var list in herolist.isSellect)
        {
            if (list == true)
            {
                count++;
            }
        }
        if (count == 0)
        {
            return;
        }

        GameManager.Instance.saveLoadManager.SaveTime();

        if (stage.text == "2-1")
        {
            GameManager.Instance.ChanageScene("stage2-1");
        }
        else if (stage.text == "2-2")
        {
            GameManager.Instance.ChanageScene("stage2-2");
        }
    }

    public void exitBtn()
    {
        gameObject.SetActive(false);
    }

    public void ShowCharacterSelectWindow()
    {
        gameObject.SetActive(true);
    }
}
