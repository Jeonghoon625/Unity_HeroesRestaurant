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

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        
    }

    private void Init()
    {
        resourceMgr.LoadCurrency(currencySection, currencyPrefab);
        resourceMgr.LoadFood(foodSection, foodPrefab);
    }
}
