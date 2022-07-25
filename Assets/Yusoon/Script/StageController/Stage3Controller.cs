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
        if (stage.text == "3-1")
        {
            GameManager.Instance.ChanageScene("stage3-1");
        }
        else if (stage.text == "3-2")
        {
            GameManager.Instance.ChanageScene("stage3-2");
        }
    }

    public void exitBtn()
    {
        gameObject.SetActive(false);
    }
}
