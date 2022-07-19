using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageUiController : MonoBehaviour
{
    public GameObject[] heroPrefab;
    public GameObject rewardPrefab;
    public GameObject firstRewardPrefab;
    public TextMeshProUGUI stage;


    public void stage1Btn()
    {
        //GameManager.Instance.ChangeScene("Stage1-1");
        Debug.Log("stage1-1");
    }


    public void stage2Btn()
    {
        //GameManager.Instance.ChangeScene("Stage1-2");
        Debug.Log("stage1-1");
    }
}
