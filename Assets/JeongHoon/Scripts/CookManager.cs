using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookManager : MonoBehaviour
{
    ResourceManager resourceMgr = new ResourceManager();

    public GameObject foodSection;
    public GameObject currencySection;
    public GameObject foodPrefab;
    public GameObject currencyPrefab;

    public List<FoodSlot> foodSlots = new List<FoodSlot>();
    public List<CurrencySlot> currencySlots = new List<CurrencySlot>();


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
    }
}
