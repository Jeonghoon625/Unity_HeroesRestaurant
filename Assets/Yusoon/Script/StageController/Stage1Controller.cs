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
