using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager
{
    List<Dictionary<string, object>> currencyData = new List<Dictionary<string, object>>();
    List<Dictionary<string, object>> foodData = new List<Dictionary<string, object>>();
    List<Dictionary<string, object>> recipeData = new List<Dictionary<string,object>>();
    List<Dictionary<string, object>> reserveCurrencyData = new List<Dictionary<string, object>>();
    List<Dictionary<string, object>> reserveFoodData = new List<Dictionary<string, object>>();

    CookManager cookManager;
    public void Init(CookManager cookManager)
    {
        this.cookManager = cookManager;
    }

    public void Load(GameObject currencySection, GameObject currencyPrefab, GameObject foodSection, GameObject foodPrefab)
    {
        LoadCurrency(currencySection, currencyPrefab);
        LoadFood(foodSection, foodPrefab);
        LoadRecipe();
        LoadReserve();
    }

    public void LoadCurrency(GameObject currencySection, GameObject currencyPrefab)
    {
        currencyData = CSVReader.Read("Tables\\Currency_DataTable");

        for (var i = 0; i < currencyData.Count; i++)
        {
            int id = (int)currencyData[i]["id"];
            string title = (string)currencyData[i]["name"];
            Sprite sprite = Resources.Load<Sprite>("Currency\\" + (string)currencyData[i]["image"]);

            GameObject currencyGO = Object.Instantiate(currencyPrefab, currencySection.transform);

            CurrencySlot slot = currencyGO.GetComponent<CurrencySlot>();

            slot.id = id;
            slot.title = title;
            slot.sprite = sprite;
            cookManager.currencySlots.Add(slot);
        }
    }

    public void LoadFood(GameObject foodSection, GameObject foodPrefab)
    {
        foodData = CSVReader.Read("Tables\\Food_DataTable");

        for (var i = 0; i < foodData.Count; i++)
        {
            int id = (int)foodData[i]["id"];
            string title = (string)foodData[i]["name"];
            string explanation = (string)foodData[i]["explanation"];
            int maxReserve = (int)foodData[i]["maxReserve"];
            Sprite sprite = Resources.Load<Sprite>("Food\\" + (string)foodData[i]["image"]);

            GameObject foodGO = Object.Instantiate(foodPrefab, foodSection.transform);

            FoodSlot slot = foodGO.GetComponent<FoodSlot>();

            slot.id = id;
            slot.title = title;
            slot.explanation = explanation;
            slot.sprite = sprite;
            slot.maxReserve = maxReserve;
            slot.GetComponent<Button>().onClick.AddListener(() => SelectFood(slot));
            cookManager.foodSlots.Add(slot);
        }
    }

    public void LoadRecipe()
    {
        recipeData = CSVReader.Read("Tables\\Recipe_DataTable");

        for (var i = 0; i < recipeData.Count; i++)
        {
            int foodId = (int)recipeData[i]["foodId"];

            cookManager.foodSlots[foodId].currencyList = new List<int>();

            for (int j = 0; j < cookManager.currencySlots.Count; j++)
            {
                int currencyCount = (int)recipeData[i][j.ToString()];
                cookManager.foodSlots[foodId].currencyList.Add(currencyCount);
            }
        }
    }

    public void LoadReserve()
    {
        LoadReserveCurrency();
        LoadReserveFood();
    }

    public void LoadReserveCurrency()
    {
        reserveCurrencyData = CSVReader.Read("Tables\\Currency_ReserveTable");

        for (var i = 0; i < reserveCurrencyData.Count; i++)
        {
            int currencyId = (int)reserveCurrencyData[i]["currencyId"];

            cookManager.currencyReserve.Insert(currencyId, (int)reserveCurrencyData[i]["reserve"]);
        }

        for (var i = 0; i < cookManager.currencySlots.Count; i++)
        {
            cookManager.currencySlots[i].reserveText.text = cookManager.currencyReserve[i].ToString();
        }
    }

    public void LoadReserveFood()
    {
        reserveFoodData = CSVReader.Read("Tables\\Food_ReserveTable");

        for (var i = 0; i < reserveFoodData.Count; i++)
        {
            int foodId = (int)reserveFoodData[i]["foodId"];

            cookManager.foodReserve.Insert(foodId, (int)reserveFoodData[i]["reserve"]);
        }
    }

    public void SelectFood(FoodSlot slot)
    {
        cookManager.selectFood = slot;
        cookManager.infoMgr.ShowInfo(slot,cookManager);
    }
}
