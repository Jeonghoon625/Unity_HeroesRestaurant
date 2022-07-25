using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellManager : MonoBehaviour
{
    bool isInit = false;

    List<bool> foodIsSells = new List<bool>();

    CookManager cookManager;

    public CookManager CookMenu;

    private void Start()
    {
        CookMenu.Init();
    }

    public void Init(CookManager cookManager)
    {
        Debug.Log("Sell 초기화");
        this.cookManager = cookManager;

        for (int i = 0; i < GameManager.Instance.resourceManager.foodData.Count; i++)
        {
            foodIsSells.Add(false);
        }

        Debug.Log(foodIsSells.Count);
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
        //cool /= 5;
        //Debug.Log(cookManager.uiManager.foodSlots[foodId].title + " 판매 시작");

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

        //Debug.Log(cookManager.uiManager.foodSlots[foodId].title + " 판매 완료"); 
    }
    
    public void TimeSell(double times)
    {
        Debug.Log("불러부러");
        Debug.Log(times);
        for (var i = 0; i < GameManager.Instance.goodsManager.foodReserve.Count; i++)
        {
            if(GameManager.Instance.goodsManager.foodReserve[i] > 0)
            {
                int foodId = i;
                int sellTime = cookManager.uiManager.foodSlots[foodId].sellTime;
                int sellSum = (int)(times / sellTime);

                if (GameManager.Instance.goodsManager.foodReserve[i] >= sellSum)
                {

                    GameManager.Instance.goodsManager.foodReserve[foodId] -= sellSum;
                    GameManager.Instance.goodsManager.gold += sellSum * cookManager.uiManager.foodSlots[foodId].sellGold;

                    Debug.Log(cookManager.uiManager.foodSlots[i].title + sellSum + "개" + sellSum * cookManager.uiManager.foodSlots[foodId].sellGold + "원" + "시간차 판매");
                    cookManager.uiManager.informationPanel.UpdateReserve();
                }
                else if (GameManager.Instance.goodsManager.foodReserve[i] < sellSum)
                {
                    Debug.Log(cookManager.uiManager.foodSlots[i].title + GameManager.Instance.goodsManager.foodReserve[i] + "개" +
                       GameManager.Instance.goodsManager.foodReserve[i] * cookManager.uiManager.foodSlots[foodId].sellGold + "원" + "시간차 판매");

                    GameManager.Instance.goodsManager.gold += GameManager.Instance.goodsManager.foodReserve[i] * cookManager.uiManager.foodSlots[foodId].sellGold;
                    GameManager.Instance.goodsManager.foodReserve[i] = 0;
                   
                    cookManager.uiManager.informationPanel.UpdateReserve();
                }
            }
        }
    }
}
