using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public CookManager cookManager;

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

        for (var i = 0; i < GameManager.Instance.resourceManager.foodData.Count; i++)
        {
            int id = (int)GameManager.Instance.resourceManager.foodData[i]["id"];
            string title = (string)GameManager.Instance.resourceManager.foodData[i]["name"];
            string explanation = (string)GameManager.Instance.resourceManager.foodData[i]["explanation"];
            int maxReserve = (int)GameManager.Instance.resourceManager.foodData[i]["maxReserve"];
            Sprite sprite = Resources.Load<Sprite>("Food\\" + (string)GameManager.Instance.resourceManager.foodData[i]["image"]);

            GameObject foodGO = Object.Instantiate(foodPrefab, foodSection.transform);
            FoodSlot slot = foodGO.GetComponent<FoodSlot>();
            slot.id = id;
            slot.title = title;
            slot.explanation = explanation;
            slot.sprite = sprite;
            slot.maxReserve = maxReserve;
            slot.GetComponent<Button>().onClick.AddListener(() => SelectFood(slot));

            foodSlots.Add(slot);
        }

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

        for (var i = 0; i < GameManager.Instance.resourceManager.reserveCurrencyData.Count; i++)
        {
            int currencyId = (int)GameManager.Instance.resourceManager.reserveCurrencyData[i]["currencyId"];

            GameManager.Instance.goodsManager.currencyReserve.Insert(currencyId, (int)GameManager.Instance.resourceManager.reserveCurrencyData[i]["reserve"]);
        }

        for (var i = 0; i < currencySlots.Count; i++)
        {
            currencySlots[i].reserveText.text = GameManager.Instance.goodsManager.currencyReserve[i].ToString();
        }

        for (var i = 0; i < GameManager.Instance.resourceManager.reserveFoodData.Count; i++)
        {
            int foodId = (int)GameManager.Instance.resourceManager.reserveFoodData[i]["foodId"];

            GameManager.Instance.goodsManager.foodReserve.Insert(foodId, (int)GameManager.Instance.resourceManager.reserveFoodData[i]["reserve"]);
        }
    }

   public void SelectFood(FoodSlot slot)
   {
       cookManager.selectFood = slot;
       informationPanel.ShowInfo(slot, this);
   }
}
