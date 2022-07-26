using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellManager : MonoBehaviour
{
    bool isInit = false;

    List<bool> foodIsSells = new List<bool>();

    CookManager cookManager;

    public CookManager CookMenu;

    public List<sellSlot> sellSlots = new List<sellSlot>();

    public GameObject soldOutPanel;

    public GameObject itemPrefab;

    public Transform itemSection;

    private bool isSell = false;

    public void GenerateSellSlots(int foodId, int sum)
    {
        if(sum >= 1)
        {
            Debug.Log("아디" + foodId);
            Debug.Log("합" + sum);

            Sprite sprite = Resources.Load<Sprite>("Food\\" + (string)GameManager.Instance.resourceManager.foodData[foodId]["image"]); ;

            GameObject sellGO = Object.Instantiate(itemPrefab, itemSection.transform);
            sellSlot slot = itemPrefab.GetComponent<sellSlot>();

            slot.sprite = sprite;
            slot.sum = sum;

            sellSlots.Add(slot);
            isSell = true;
        }
    }

    public void DestroySellSlots()
    {
        for(int i = 0; i < sellSlots.Count; i++)
        {
            Destroy(sellSlots[i].GetComponent<GameObject>());
        }

        sellSlots.Clear();
    }

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
                    
                    GenerateSellSlots(foodId, sellSum);
                    //Debug.Log(cookManager.uiManager.foodSlots[i].title + sellSum + "개" + sellSum * cookManager.uiManager.foodSlots[foodId].sellGold + "원" + "시간차 판매");
                    cookManager.uiManager.informationPanel.UpdateReserve();

                    
                }
                else if (GameManager.Instance.goodsManager.foodReserve[i] < sellSum)
                {
                    //Debug.Log(cookManager.uiManager.foodSlots[i].title + GameManager.Instance.goodsManager.foodReserve[i] + "개" +
                    //GameManager.Instance.goodsManager.foodReserve[i] * cookManager.uiManager.foodSlots[foodId].sellGold + "원" + "시간차 판매");
                    int sellSum2 = GameManager.Instance.goodsManager.foodReserve[i];
                
                    GenerateSellSlots(foodId, sellSum2);
                    GameManager.Instance.goodsManager.gold += GameManager.Instance.goodsManager.foodReserve[i] * cookManager.uiManager.foodSlots[foodId].sellGold;
                    GameManager.Instance.goodsManager.foodReserve[i] = 0;
                   
                    cookManager.uiManager.informationPanel.UpdateReserve();
                }
            }
        }

        GenerateSellSlots(GameManager.Instance.goodsManager.foodReserve.Count + 1, 0);

        if (isSell)
        {
            soldOutPanel.SetActive(true);
        }
    }

    public void OffPanel(GameObject panel)
    {
        DestroySellSlots();
        panel.SetActive(false);
    }
}
