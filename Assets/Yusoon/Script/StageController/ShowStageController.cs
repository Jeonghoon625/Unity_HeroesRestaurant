using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowStageController : MonoBehaviour
{
    public GameObject[] stages;

    public void Stage1Btn()
    {
        stages[0].gameObject.SetActive(true);
    }
    public void Stage2Btn()
    {
        stages[1].gameObject.SetActive(true);
    }
    public void Stage3Btn()
    {
        stages[2].gameObject.SetActive(true);
    }

}
