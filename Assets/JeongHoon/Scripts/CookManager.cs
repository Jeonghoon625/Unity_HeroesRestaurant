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
        GameManager.Instance.Init();

        GameManager.Instance.saveLoadManager.Load();

        uiManager.Init();
        sellManager.Init(this);
    }

    private void OnEnable()
    {
        uiManager.UpdateStageState();
    }

    private void Update()
    {
        uiManager.UpdateSelectLight();
        uiManager.UpdateSoldOut();
    }
}
