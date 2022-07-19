using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryUiManager : MonoBehaviour
{
    [SerializeField] GameObject[] stages;
    int currentStage = 0;

    public void NextStage()
    {
        if (currentStage < stages.Length - 1)
        {
            stages[currentStage++].SetActive(false);
            stages[currentStage].SetActive(true);
        }
    }
}
