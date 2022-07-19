using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InformationLoadManager : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI explanation;
    public Image image;

    public GameObject[] RecipeCurrencySection;

    public TextMeshProUGUI currentReserve;
    public TextMeshProUGUI maxReserve;

    public Slider reserveSlider;

    public void Init()
    {
        for (int i = 0; i < RecipeCurrencySection.Length; i++)
        {
            RecipeCurrencySection[i].SetActive(false);
        }
    }

    public void ShowInfo(FoodSlot selectSlot, CookManager cookManager)
    {
        FoodSlot slotInfo = selectSlot;

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

            currentReserve.text = cookManager.foodReserve[slotInfo.id].ToString();
            maxReserve.text = slotInfo.maxReserve.ToString();

            reserveSlider.minValue = 0;
            reserveSlider.maxValue = slotInfo.maxReserve;
            reserveSlider.value = cookManager.foodReserve[slotInfo.id];
        }
    }
}
