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
            //GameManager.Instance.ChangeScene("Stage1-1");
            Debug.Log("1-1");
        }
        else if (stage.text == "1-2")
        {
            //GameManager.Instance.ChangeScene("Stage1-2");
            Debug.Log("1-2");
        }
    }

    public void exitBtn()
    {
        gameObject.SetActive(false);
    }
}
