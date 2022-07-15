using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuLoadManager : MonoBehaviour
{
    List<Dictionary<string, object>> foodData = new List<Dictionary<string, object>>();
    public GameObject foodSection;
    public GameObject foodPrefab;

    private void Awake()
    {
        foodData = CSVReader.Read("Tables\\Food_DataTable");
        
        for (var i = 0; i < foodData.Count; i++)
        {
            int id = (int)foodData[i]["id"];
            string title = (string)foodData[i]["name"];
            string explanation = (string)foodData[i]["explanation"];
            Sprite sprite = Resources.Load<Sprite>("Food\\" + (string)foodData[i]["image"]);

            GameObject foodGO = Instantiate(foodPrefab);
            foodGO.transform.SetParent(foodSection.transform);

            FoodSlot slot = foodGO.GetComponent<FoodSlot>();

            slot.id = id;
            slot.title = title;
            slot.explanation = explanation;
            slot.sprite = sprite;
        }
    }

}
