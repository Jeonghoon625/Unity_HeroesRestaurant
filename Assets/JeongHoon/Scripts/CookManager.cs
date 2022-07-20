using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookManager : MonoBehaviour
{
    public InformationPanel informationPanel;

    public GameObject foodSection;
    public GameObject currencySection;

    public List<FoodSlot> foodSlots = new List<FoodSlot>();
    public List<CurrencySlot> currencySlots = new List<CurrencySlot>();

    public GameObject foodPrefab;
    public GameObject currencyPrefab;

    public FoodSlot selectFood;

    private void OnEnable()
    {
        Init();
    }

    private void Start()
    {
        GameManager.Instance.resourceManager.Load();
    }

    private void Init()
    {
        informationPanel = GameObject.FindGameObjectWithTag("InformationPanel").GetComponent<InformationPanel>();
        foodSection = GameObject.FindGameObjectWithTag("FoodSection");
        currencySection = GameObject.FindGameObjectWithTag("CurrencySection");

        GenerateUI();

        //selectFood = GameManager.Instance.resourceManager.foodSlots[0];

        informationPanel.Init();
        informationPanel.ShowInfo(selectFood, this);
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
            //slot.GetComponent<Button>().onClick.AddListener(() => SelectFood(slot));

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
}
