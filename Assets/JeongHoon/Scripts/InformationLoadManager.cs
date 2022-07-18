using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InformationLoadManager : MonoBehaviour
{
    public CookManager cookManager;

    public TextMeshProUGUI title;
    public TextMeshProUGUI explanation;
    public Image image;

    public GameObject[] RecipeCurrencySection;

    private void Start()
    {
        for (int i = 0; i < RecipeCurrencySection.Length; i++)
        {
            RecipeCurrencySection[i].SetActive(false);
        }
    }

    public void ShowInfo()
    {
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;
        FoodSlot slotInfo = clickObject.GetComponent<FoodSlot>();

        if (slotInfo != null)
        {
            title.text = slotInfo.title;
            explanation.text = slotInfo.explanation;
            image.sprite = slotInfo.sprite;

            for(int i = 0; i < RecipeCurrencySection.Length; i++)
            {
                RecipeCurrencySection[i].SetActive(false);
            }

            int idx = 0;

            for(int i = 0; i < slotInfo.currencyList.Count; i++)
            {
                if(slotInfo.currencyList[i] != 0)
                {
                    RecipeCurrencySection[idx].SetActive(true);
                    RecipeCurrencySlot slot = RecipeCurrencySection[idx].GetComponent<RecipeCurrencySlot>();
                    slot.Change(cookManager.currencySlots[i].sprite, slotInfo.currencyList[i].ToString());
                    idx++;
                }
            }
        }
    }
}
