using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingManager : MonoBehaviour
{
    public CookManager cookManager;
    public FoodSlot selectFood;

    public void Cooking()
    {
        int produceValue = cookManager.uiManager.produceTable.quantityValue;
        this.selectFood = cookManager.selectFood;

        GameManager.Instance.goodsManager.foodReserve[selectFood.id] += produceValue;
        for (var i = 0; i < selectFood.currencyList.Count; i++)
        {
            GameManager.Instance.goodsManager.currencyReserve[i] -= selectFood.currencyList[i] * produceValue;
        }

        cookManager.uiManager.UpdateCurrencyReserveText();
        cookManager.uiManager.informationPanel.ShowInfo();
        cookManager.uiManager.produceTable.Init();
    }
}
