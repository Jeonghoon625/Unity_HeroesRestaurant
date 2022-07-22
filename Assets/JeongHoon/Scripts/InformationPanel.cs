using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InformationPanel : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI explanation;
    public Image image;

    public GameObject[] RecipeCurrencySection;

    public TextMeshProUGUI currentReserve;
    public TextMeshProUGUI maxReserve;

    public Slider reserveSlider;

    private FoodSlot slotInfo;

    public UIManager uiManager;

    public void ShowInfo(FoodSlot selectSlot, UIManager uiManager)
    {
        slotInfo = selectSlot;
        this.uiManager = uiManager;

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
                    slot.Init(uiManager.currencySlots[i].sprite, slotInfo.currencyList[i].ToString());
                    idx++;
                }
            }

            currentReserve.text = GameManager.Instance.goodsManager.foodReserve[slotInfo.id].ToString();
            maxReserve.text = slotInfo.maxReserve.ToString();

            /*
            reserveSlider.minValue = 0;
            reserveSlider.maxValue = slotInfo.maxReserve;
            reserveSlider.value = GameManager.Instance.goodsManager.foodReserve[slotInfo.id];
            */

            reserveSlider.value = 0;
            reserveSlider.minValue = 0;
            reserveSlider.maxValue = slotInfo.sellTime / 5;
        }
    }

    public void ShowInfo()
    {
        ShowInfo(this.slotInfo, this.uiManager);
    }

    public void UpdateReserve()
    {
        if(slotInfo != null)
        {
            currentReserve.text = GameManager.Instance.goodsManager.foodReserve[slotInfo.id].ToString();
        }
    }

    public void UpdateReserveSlider()
    {
        if (slotInfo != null)
        {
            reserveSlider.value = slotInfo.currentSellingTime;
        }
    }

    /*
    public void Init()
    {
        for (int i = 0; i < RecipeCurrencySection.Length; i++)
        {
            RecipeCurrencySection[i].SetActive(false);
        }
    }
    */
}
