using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum ClearReward
{
    meat = 1,
    mushroom = 2,
    fish = 3,
}
public class Reward : MonoBehaviour
{
    public GameObject rewardSlot;
    public GameObject[] prefabs;
    public GameObject wood;

    public void ClearReward(int prefabNum, int count)
    {
        var re = Instantiate(prefabs[prefabNum - 1]);
        re.transform.SetParent(rewardSlot.transform, false);
        re.GetComponentInChildren<TextMeshProUGUI>().text = $"{count}";
        Instantiate(wood).transform.SetParent(rewardSlot.transform, false);
    }
}
