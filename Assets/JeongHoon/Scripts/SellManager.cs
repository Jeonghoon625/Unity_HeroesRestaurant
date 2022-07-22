using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellManager : MonoBehaviour
{
    float timer = 0;

    bool isInit = false;

    List<bool> foodIsSells = new List<bool>();

    CookManager cookManager;

    public void Init(CookManager cookManager)
    {
        this.cookManager = cookManager;
        for (int i = 0; i < GameManager.Instance.resourceManager.foodData.Count; i++)
        {
            foodIsSells.Add(false);
        }

        isInit = true;
    }

    private void Update()
    {
        if(isInit)
        {
            cookManager.uiManager.informationPanel.UpdateReserveSlider();
            for (var i = 0; i < GameManager.Instance.goodsManager.foodReserve.Count; i++)
            {
                if (GameManager.Instance.goodsManager.foodReserve[i] > 0)
                {
                    if (!foodIsSells[i])
                    {
                        foodIsSells[i] = true;
                        StartCoroutine(Selling(cookManager.uiManager.foodSlots[i].sellTime, i));
                    }
                }
            }
        }
    }

    IEnumerator Selling(float cool, int foodId) 
    {
        cool /= 5;
        Debug.Log(cookManager.uiManager.foodSlots[foodId].title + " 판매 시작");

        while (cool > 0.0f) 
        { 
            cool -= Time.deltaTime;
            cookManager.uiManager.foodSlots[foodId].currentSellingTime += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }

        GameManager.Instance.goodsManager.foodReserve[foodId] -= 1;
        GameManager.Instance.goodsManager.gold += cookManager.uiManager.foodSlots[foodId].sellGold;
        foodIsSells[foodId] = false;
        cookManager.uiManager.foodSlots[foodId].currentSellingTime = 0;
        cookManager.uiManager.informationPanel.UpdateReserve();
        Debug.Log("판매 완료"); 
    }
    
}
