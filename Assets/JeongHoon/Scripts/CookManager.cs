using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookManager : MonoBehaviour
{
    ResourceManager resourceMgr = new ResourceManager();
    public InformationPanel informationPanel;

    public GameObject foodSection;
    public GameObject currencySection;
    public GameObject foodPrefab;
    public GameObject currencyPrefab;

    public List<FoodSlot> foodSlots = new List<FoodSlot>();
    public List<CurrencySlot> currencySlots = new List<CurrencySlot>();

    public List<int> currencyReserve = new List<int>();
    public List<int> foodReserve = new List<int>();

    public FoodSlot selectFood;

    private void Awake()
    {
        
    }

    private void Start()
    {
        Init();

    }

    private void Init()
    {
        Debug.Log("CookMgrInit");
        informationPanel = GameObject.FindGameObjectWithTag("InformationPanel").GetComponent<InformationPanel>();
        foodSection = GameObject.FindGameObjectWithTag("FoodSection");
        currencySection = GameObject.FindGameObjectWithTag("CurrencySection");

        resourceMgr.Init();
        resourceMgr.Load(currencySection, currencyPrefab, foodSection, foodPrefab);
        selectFood = foodSlots[0];
        informationPanel.Init();
        informationPanel.ShowInfo(selectFood, this);
    }
}
