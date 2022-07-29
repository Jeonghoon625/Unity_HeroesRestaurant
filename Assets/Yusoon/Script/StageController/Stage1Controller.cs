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
    public TextMeshProUGUI reward;
    public CharacterSelectController characterSelectController;
    public HeroList herolist;

    public GameObject cannotSelect;

    private void OnEnable()
    {
        herolist.isSellect[2] = false;
        herolist.isSellect[3] = false;

        heroPrefab[2].SetActive(false);
        heroPrefab[3].SetActive(false);
    }
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
        reward.text = "50";
        //properCompatPower.text = "3";
    }

    public void stage2Btn()
    {
        stage.text = "1-2";
        reward.text = "120";
        //properCompatPower.text = "3";
    }

    public void StartStage1Btn()
    {
        var gameManager = GameManager.Instance;

        if(gameManager.goodsManager.stamina <= 0)
        {
            Debug.Log("�� �����...");
            return;
        }
        gameManager.goodsManager.stamina--;

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

        gameManager.saveLoadManager.SaveTime();

        if (stage.text == "1-1")
        {
            gameManager.ChanageScene("Gang");
        }
        else if (stage.text == "1-2")
        {
            gameManager.ChanageScene("stage1-2");
        }
    }

    public void CannotSelect()
    {
        cannotSelect.gameObject.SetActive(true);
        Invoke("UnActive", 1.5f);
    }

    public void UnActive()
    {
        cannotSelect.gameObject.SetActive(false);
    }
    public void exitBtn()
    {
        gameObject.SetActive(false);
    }
}
