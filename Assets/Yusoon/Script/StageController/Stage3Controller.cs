using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Stage3Controller : MonoBehaviour
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
        stage.text = "3-1";
        properCompatPower.text = "8";
    }

    public void stage2Btn()
    {
        stage.text = "3-2";
        properCompatPower.text = "11";
    }

    public void StartStageBtn()
    {
        var gameManager = GameManager.Instance;

        if (gameManager.goodsManager.stamina <= 0)
        {
            Debug.Log("³ª Èûµé¾î...");
            return;
        }
        gameManager.goodsManager.stamina--;

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

        gameManager.saveLoadManager.SaveTime();

        if (stage.text == "3-1")
        {
            gameManager.ChanageScene("stage3-1");
        }
        else if (stage.text == "3-2")
        {
            gameManager.ChanageScene("stage3-2");
        }
    }

    public void exitBtn()
    {
        gameObject.SetActive(false);
    }
}
