using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    List<Dictionary<string, object>> currencyData = new List<Dictionary<string, object>>();
    List<Dictionary<string, object>> foodData = new List<Dictionary<string, object>>();

    public void LoadCurrency(GameObject currencySection, GameObject currencyPrefab)
    {
        currencyData = CSVReader.Read("Tables\\Currency_DataTable");

        for (var i = 0; i < currencyData.Count; i++)
        {
            Debug.Log(currencyData[i].ToString());
            int id = (int)currencyData[i]["id"];
            string title = (string)currencyData[i]["name"];
            Sprite sprite = Resources.Load<Sprite>("Currency\\" + (string)currencyData[i]["image"]);

            GameObject currencyGO = Object.Instantiate(currencyPrefab, currencySection.transform);

            CurrencySlot slot = currencyGO.GetComponent<CurrencySlot>();

            slot.id = id;
            slot.title = title;
            slot.sprite = sprite;
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
            Sprite sprite = Resources.Load<Sprite>("Food\\" + (string)foodData[i]["image"]);

            GameObject foodGO = Object.Instantiate(foodPrefab, foodSection.transform);

            FoodSlot slot = foodGO.GetComponent<FoodSlot>();

            slot.id = id;
            slot.title = title;
            slot.explanation = explanation;
            slot.sprite = sprite;
        }
    }

    
}
