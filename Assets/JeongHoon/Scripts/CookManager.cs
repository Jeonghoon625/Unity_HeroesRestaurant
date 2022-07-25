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

        System.DateTime StartDate = System.DateTime.Now;
        Debug.Log(StartDate);
        System.DateTime EndDate = GameManager.Instance.saveLoadManager.LoadTime();
        Debug.Log(EndDate);
        System.TimeSpan timeCal = StartDate - EndDate;
        Debug.Log(timeCal);
        sellManager.TimeSell(timeCal.TotalSeconds);
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
