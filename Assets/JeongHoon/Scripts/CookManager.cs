using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookManager : MonoBehaviour
{
    ResourceManager resourceMgr = new ResourceManager();
    public InformationLoadManager infoMgr;

    public GameObject foodSection;
    public GameObject currencySection;
    public GameObject foodPrefab;
    public GameObject currencyPrefab;

    public List<FoodSlot> foodSlots = new List<FoodSlot>();
    public List<CurrencySlot> currencySlots = new List<CurrencySlot>();

    public FoodSlot selectFood;

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        
    }

    private void Init()
    {
        resourceMgr.Init(this);
        resourceMgr.Load(currencySection, currencyPrefab, foodSection, foodPrefab);
        selectFood = foodSlots[0];
        infoMgr.Init();
        infoMgr.ShowInfo(selectFood, this);
    }
}
