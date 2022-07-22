using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public CookManager cookManager;

    //Produce Panel
    public ProduceTable produceTable;

    //Information Panel
    public InformationPanel informationPanel;

    //Slot Prefabs
    public GameObject foodPrefab;
    public GameObject currencyPrefab;

    //Slots
    public List<FoodSlot> foodSlots = new List<FoodSlot>();
    public List<CurrencySlot> currencySlots = new List<CurrencySlot>();

    //Section for Slots
    public GameObject foodSection;
    public GameObject currencySection;

    public void Init()
    {
        GenerateUI();
        cookManager.selectFood = foodSlots[0];
        informationPanel.ShowInfo(cookManager.selectFood, this);
    }

    private void GenerateUI()
    {
        //재료
        for (var i = 0; i < GameManager.Instance.resourceManager.currencyData.Count; i++)
        {
            int id = (int)GameManager.Instance.resourceManager.currencyData[i]["id"];
            string title = (string)GameManager.Instance.resourceManager.currencyData[i]["name"];
            Sprite sprite = Resources.Load<Sprite>("Currency\\" + (string)GameManager.Instance.resourceManager.currencyData[i]["image"]);

            GameObject currencyGO = Object.Instantiate(currencyPrefab, currencySection.transform);
            CurrencySlot slot = currencyGO.GetComponent<CurrencySlot>();
            slot.id = id;
            slot.title = title;
            slot.sprite = sprite;

            currencySlots.Add(slot);
        }

        //음식
        for (var i = 0; i < GameManager.Instance.resourceManager.foodData.Count; i++)
        {
            int id = (int)GameManager.Instance.resourceManager.foodData[i]["id"];
            string title = (string)GameManager.Instance.resourceManager.foodData[i]["name"];
            string explanation = (string)GameManager.Instance.resourceManager.foodData[i]["explanation"];
            int maxReserve = (int)GameManager.Instance.resourceManager.foodData[i]["maxReserve"];
            Sprite sprite = Resources.Load<Sprite>("Food\\" + (string)GameManager.Instance.resourceManager.foodData[i]["image"]);
            int stage = (int)GameManager.Instance.resourceManager.foodData[i]["stage"];

            int sellTime = (int)GameManager.Instance.resourceManager.foodData[i]["sellTime"];
            int sellGold = (int)GameManager.Instance.resourceManager.foodData[i]["sellGold"];

            GameObject foodGO = Object.Instantiate(foodPrefab, foodSection.transform);
            FoodSlot slot = foodGO.GetComponent<FoodSlot>();
            slot.id = id;
            slot.title = title;
            slot.explanation = explanation;
            slot.sprite = sprite;
            slot.maxReserve = maxReserve;
            slot.stage = stage;
            slot.sellTime = sellTime;
            slot.sellGold = sellGold;

            slot.GetComponent<Button>().onClick.AddListener(() => SelectFood(slot));

            foodSlots.Add(slot);
        }

        //레시피
        for (var i = 0; i < GameManager.Instance.resourceManager.recipeData.Count; i++)
        {
            int foodId = (int)GameManager.Instance.resourceManager.recipeData[i]["foodId"];

            foodSlots[foodId].currencyList = new List<int>();

            for (int j = 0; j < currencySlots.Count; j++)
            {
                int currencyCount = (int)GameManager.Instance.resourceManager.recipeData[i][j.ToString()];
                foodSlots[foodId].currencyList.Add(currencyCount);
            }
        }

        //재료 보유량
        for (var i = 0; i < currencySlots.Count; i++)
        {
            currencySlots[i].reserveText.text = GameManager.Instance.goodsManager.currencyReserve[i].ToString();
        }
    }

    public void UpdateStageState()
    {
        for(var i = 0; i < foodSlots.Count; i++)
        {
            if(GameManager.Instance.masterStage < foodSlots[i].stage) //Lock
            {
                foodSlots[i].Lock();
            }
            else //Open
            {
                foodSlots[i].Open();
            }
        }
    }

    public void UpdateCurrencyReserveText()
    {
        for (var i = 0; i < currencySlots.Count; i++)
        {
            currencySlots[i].reserveText.text = GameManager.Instance.goodsManager.currencyReserve[i].ToString();
        }
    }

    public void UpdateSelectLight()
    {
        for (var i = 0; i < foodSlots.Count; i++)
        {
            if (cookManager.selectFood.id == foodSlots[i].id) 
            {
                foodSlots[i].OnLight();
            }
            else //Open
            {
                foodSlots[i].OffLight();
            }
        }
    }

    public void UpdateSoldOut()
    {
        for (var i = 0; i < foodSlots.Count; i++)
        {
            if(!foodSlots[i].lockGO.activeSelf)
            {
                if (GameManager.Instance.goodsManager.foodReserve[foodSlots[i].id] == 0)
                {
                    foodSlots[i].image.color = new Color32(90, 90, 90, 255);
                }
                else
                {
                    foodSlots[i].image.color = Color.white;
                }
            }
        }
    }

    public void SelectFood(FoodSlot slot)
    {
        cookManager.selectFood = slot;
        informationPanel.ShowInfo(slot, this);
        produceTable.Init();
    }
}
