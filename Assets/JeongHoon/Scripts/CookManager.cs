using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookManager : MonoBehaviour
{
    public FoodSlot selectFood;

    public UIManager uiManager;

    public CookingManager cookingManager;

    public SellManager sellManager;

    private void Awake()

    {
        if(!GameManager.Instance.isCookInit)
        {
            GameManager.Instance.saveLoadManager.Load();
            GameManager.Instance.CookInit();
        }

        uiManager.Init();
        sellManager.Init(this);
        uiManager.UpdateStageState();

        if (GameManager.Instance.saveLoadManager.LoadTime() != -1)
        {
            sellManager.TimeSell(System.DateTime.Now.TimeOfDay.TotalSeconds - GameManager.Instance.saveLoadManager.LoadTime() + 40);
        }
    }

    private void OnEnable()
    {
        //uiManager.UpdateStageState();
    }

    private void Update()
    {
        uiManager.UpdateSelectLight();
        uiManager.UpdateSoldOut();
    }
}
