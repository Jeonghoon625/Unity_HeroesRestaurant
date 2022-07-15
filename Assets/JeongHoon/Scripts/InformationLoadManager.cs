using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InformationLoadManager : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI explanation;
    public Image image;

    public void ShowInfo()
    {
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;
        FoodSlot slotInfo = clickObject.GetComponent<FoodSlot>();

        if (slotInfo != null)
        {
            title.text = slotInfo.title;
            explanation.text = slotInfo.explanation;
            image.sprite = slotInfo.sprite;
        }
    }
}
