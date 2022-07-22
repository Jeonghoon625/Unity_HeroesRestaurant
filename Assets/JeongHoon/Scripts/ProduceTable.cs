using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProduceTable : MonoBehaviour
{
    public CookManager cookManager;
    public Slider produceSlider;
    public TextMeshProUGUI quantity;
    public GameObject[] recipeCurrencySlots;
    public int quantityValue = 0;

    private void Update()
    {
        if (cookManager.selectFood.maxReserve - GameManager.Instance.goodsManager.foodReserve[cookManager.selectFood.id] == 0)
        {
            produceSlider.minValue = 0;
            produceSlider.maxValue = 0;
            quantityValue = 0;
        }
        else
        {
            produceSlider.minValue = 1;
            produceSlider.maxValue = cookManager.selectFood.maxReserve - GameManager.Instance.goodsManager.foodReserve[cookManager.selectFood.id];
            quantityValue = (int)produceSlider.value;

            for (var i = 0; i < recipeCurrencySlots.Length; i++)
            {
                if (recipeCurrencySlots[i].activeSelf)
                {
                    int initialQuantity = recipeCurrencySlots[i].GetComponent<RecipeCurrencySlot>().initialQuantity;
                    int temp = initialQuantity * quantityValue;
                    recipeCurrencySlots[i].GetComponent<RecipeCurrencySlot>().currentQuantity.text = temp.ToString();
                }
            }
        }

        quantity.text = quantityValue.ToString();
    }

    public void Init()
    {
        produceSlider.value = 0;
    }

    public Button CookButton;
    public Button CookMaxButton;
}

