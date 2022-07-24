using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

public class SaveLoadManager
{
    private FileInfo fileInfo;
    public bool isInit = false;

    public void Save()
    {
        SaveReserve();
    }

    public void Load()
    {
        LoadReserve();
        isInit = true;
    }

    public void LoadReserve()
    {
        LoadFoodReserve();
        LoadCurrencyReserve();
    }

    public void SaveReserve()
    {
        SaveFoodReserve();
        SaveCurrencyReserve();
    }

    public void GenerateDefalutCurrencyReserve()
    {
        Debug.Log($"초기 재료 보유량 데이터 생성");

        //Currency 보유량
        string fileName = "CurrencyReserve";
        string path = Application.dataPath + fileName + ".Json";

        for (var i = 0; i < GameManager.Instance.resourceManager.currencyData.Count; i++)
        {
            int currencyId = i;

            GameManager.Instance.goodsManager.currencyReserve.Insert(currencyId, 999);
        }

        var setJson = JsonConvert.SerializeObject(GameManager.Instance.goodsManager.currencyReserve);
        File.WriteAllText(path, setJson);
    }

    public void GenerateDefaultFoodReserve()
    {
        Debug.Log($"초기 음식 보유량 데이터 생성");

        //Food 보유량
        string fileName = "FoodReserve";
        string path = Application.dataPath + fileName + ".Json";
        for (var i = 0; i < GameManager.Instance.resourceManager.foodData.Count; i++)
        {
            int foodId = i;

            GameManager.Instance.goodsManager.foodReserve.Insert(foodId, 10);
        }

        var setJson = JsonConvert.SerializeObject(GameManager.Instance.goodsManager.foodReserve);
        File.WriteAllText(path, setJson);
    }

    public void LoadFoodReserve()
    {
        Debug.Log("음식 보유량 로드");

        string fileName = "FoodReserve";
        string path = Application.dataPath + fileName + ".Json";

        fileInfo = new FileInfo(path);

        if (!fileInfo.Exists)
        {
            GenerateDefaultFoodReserve();
        }
        else
        {
            string json = File.ReadAllText(path);
            GameManager.Instance.goodsManager.foodReserve = JsonConvert.DeserializeObject<List<int>>(json);
        }
    }

    public void LoadCurrencyReserve()
    {
        Debug.Log("재료 보유량 로드");

        string fileName = "CurrencyReserve";
        string path = Application.dataPath + fileName + ".Json";

        fileInfo = new FileInfo(path);

        if (!fileInfo.Exists)
        {
            GenerateDefalutCurrencyReserve();
        }
        else
        {
            string json = File.ReadAllText(path);
            GameManager.Instance.goodsManager.currencyReserve = JsonConvert.DeserializeObject<List<int>>(json);
        }
    }

    public void SaveFoodReserve()
    {
        //Food 보유량
        string fileName = "FoodReserve";
        string path = Application.dataPath + fileName + ".Json";

        var setJson = JsonConvert.SerializeObject(GameManager.Instance.goodsManager.foodReserve);
        File.WriteAllText(path, setJson);
    }

    public void SaveCurrencyReserve()
    {
        //Currency 보유량
        string fileName = "CurrencyReserve";
        string path = Application.dataPath + fileName + ".Json";

        var setJson = JsonConvert.SerializeObject(GameManager.Instance.goodsManager.currencyReserve);
        File.WriteAllText(path, setJson);
    }
}
