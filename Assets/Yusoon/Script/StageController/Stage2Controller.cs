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
        if (stage.text == "2-1")
        {
            //GameManager.Instance.ChangeScene("Stage1-1");
            Debug.Log("2-1");
        }
        else if (stage.text == "2-2")
        {
            //GameManager.Instance.ChangeScene("Stage1-2");
            Debug.Log("2-2");
        }
    }

    public void exitBtn()
    {
        gameObject.SetActive(false);
    }
}
