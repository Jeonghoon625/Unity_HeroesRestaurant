using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookManager : MonoBehaviour
{
    public FoodSlot selectFood;

    public UIManager uiManager;

    private void Awake()
    {
        uiManager.Init();
    }

    private void OnEnable()
    {
        Debug.Log("È°¼º");
    }

    private void Start()
    {
        
    }
}
