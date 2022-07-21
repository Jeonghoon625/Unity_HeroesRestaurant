using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsManager
{
    public List<int> currencyReserve = new List<int>();
    public List<int> foodReserve = new List<int>();

    public void Load()
    {
        Debug.Log("로드했음");
        for (var i = 0; i < GameManager.Instance.resourceManager.reserveCurrencyData.Count; i++)
        {
            int currencyId = (int)GameManager.Instance.resourceManager.reserveCurrencyData[i]["currencyId"];

            currencyReserve.Insert(currencyId, (int)GameManager.Instance.resourceManager.reserveCurrencyData[i]["reserve"]);
        }

        for (var i = 0; i < GameManager.Instance.resourceManager.reserveFoodData.Count; i++)
        {
            int foodId = (int)GameManager.Instance.resourceManager.reserveFoodData[i]["foodId"];

            foodReserve.Insert(foodId, (int)GameManager.Instance.resourceManager.reserveFoodData[i]["reserve"]);
        }
    }
}