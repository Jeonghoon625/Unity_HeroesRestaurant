using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
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
        Debug.Log("SaveData");
        SaveReserve();
        SaveGold();
        SaveWood();
        SaveStageInfo();
        SaveTime();
    }

    public void Load()
    {
        LoadReserve();
        LoadGold();
        LoadWood();
        LoadStageInfo();
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

    public void SaveTime()
    {
        Debug.Log("시간 저장");
        string fileName = "Time";
        string path = Application.persistentDataPath + "/" + fileName + ".Json";

        var setJson = JsonConvert.SerializeObject(System.DateTime.Now);
        File.WriteAllText(path, setJson);
    }

    public System.DateTime LoadTime()
    {
        Debug.Log("시간 로드");

        string fileName = "Time";
        string path = Application.persistentDataPath + "/" + fileName + ".Json";

        fileInfo = new FileInfo(path);

        if (!fileInfo.Exists)
        {
            SaveTime();
            return System.DateTime.Now;
        }
        else
        {
            string json = File.ReadAllText(path);
            System.DateTime times = JsonConvert.DeserializeObject<System.DateTime>(json);
            return times;
        }
    }

    /*
    public void GenerateDefaultProduceTime()
    {
        Debug.Log($"초기 제작 시간 생성");

        //Currency 보유량
        string fileName = "ProduceTime";
        string path = Application.dataPath + fileName + ".Json";

        for (var i = 0; i < GameManager.Instance.resourceManager.currencyData.Count; i++)
        {
            int currencyId = i;

            GameManager.Instance.goodsManager.currencyReserve.Insert(currencyId, 999);
        }

        var setJson = JsonConvert.SerializeObject(GameManager.Instance.goodsManager.currencyReserve);
        File.WriteAllText(path, setJson);
    }
    */

    public void GenerateDefaultCurrencyReserve()
    {
        Debug.Log($"초기 재료 보유량 데이터 생성");

        //Currency 보유량
        string fileName = "CurrencyReserve";
        string path = Application.persistentDataPath + "/" + fileName + ".Json";

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
        string path = Application.persistentDataPath + "/" + fileName + ".Json";
        for (var i = 0; i < GameManager.Instance.resourceManager.foodData.Count; i++)
        {
            int foodId = i;

            GameManager.Instance.goodsManager.foodReserve.Insert(foodId, 0);
        }

        var setJson = JsonConvert.SerializeObject(GameManager.Instance.goodsManager.foodReserve);
        File.WriteAllText(path, setJson);
    }

    public void LoadFoodReserve()
    {
        Debug.Log("음식 보유량 로드");

        string fileName = "FoodReserve";
        string path = Application.persistentDataPath + "/" + fileName + ".Json";

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
        string path = Application.persistentDataPath + "/" + fileName + ".Json";

        fileInfo = new FileInfo(path);

        if (!fileInfo.Exists)
        {
            GenerateDefaultCurrencyReserve();
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
        string path = Application.persistentDataPath + "/" + fileName + ".Json";

        var setJson = JsonConvert.SerializeObject(GameManager.Instance.goodsManager.foodReserve);
        File.WriteAllText(path, setJson);
    }

    public void SaveCurrencyReserve()
    {
        //Currency 보유량
        string fileName = "CurrencyReserve";
        string path = Application.persistentDataPath + "/" + fileName + ".Json";

        var setJson = JsonConvert.SerializeObject(GameManager.Instance.goodsManager.currencyReserve);
        File.WriteAllText(path, setJson);
    }

    public void SaveGold()
    {
        string fileName = "Gold";
        string path = Application.persistentDataPath + "/" + fileName + ".Json";

        Debug.Log(path);
        var setJson = JsonConvert.SerializeObject(GameManager.Instance.goodsManager.gold);
        File.WriteAllText(path, setJson);
    }

    public void LoadGold()
    {
        string fileName = "Gold";
        string path = Application.persistentDataPath + "/" + fileName + ".Json";

        fileInfo = new FileInfo(path);

        if (!fileInfo.Exists)
        {
            GameManager.Instance.goodsManager.gold = 1000;
            var setJson = JsonConvert.SerializeObject(GameManager.Instance.goodsManager.gold);
            File.WriteAllText(path, setJson);
        }
        else
        {
            string json = File.ReadAllText(path);
            GameManager.Instance.goodsManager.gold = JsonConvert.DeserializeObject<int>(json);
        }
    }

    public void SaveWood()
    {
        string fileName = "Wood";
        string path = Application.persistentDataPath + "/" + fileName + ".Json";

        var setJson = JsonConvert.SerializeObject(GameManager.Instance.goodsManager.wood);
        File.WriteAllText(path, setJson);
    }

    public void LoadWood()
    {
        string fileName = "Wood";
        string path = Application.persistentDataPath + "/" + fileName + ".Json";

        fileInfo = new FileInfo(path);

        if (!fileInfo.Exists)
        {
            GameManager.Instance.goodsManager.wood = 500;
            var setJson = JsonConvert.SerializeObject(GameManager.Instance.goodsManager.wood);
            File.WriteAllText(path, setJson);
        }
        else
        {
            string json = File.ReadAllText(path);
            GameManager.Instance.goodsManager.wood = JsonConvert.DeserializeObject<int>(json);
        }
    }

    public void SaveStageInfo()
    {
        string fileName = "StageInfo";
        string path = Application.persistentDataPath + "/" + fileName + ".Json";

        var setJson = JsonConvert.SerializeObject(GameManager.Instance.masterStage);
        File.WriteAllText(path, setJson);
    }

    public void LoadStageInfo()
    {
        string fileName = "StageInfo";
        string path = Application.persistentDataPath + "/" + fileName + ".Json";

        fileInfo = new FileInfo(path);

        if (!fileInfo.Exists)
        {
            GameManager.Instance.masterStage = 1;
            var setJson = JsonConvert.SerializeObject(GameManager.Instance.masterStage);
            File.WriteAllText(path, setJson);
        }
        else
        {
            string json = File.ReadAllText(path);
            GameManager.Instance.masterStage = JsonConvert.DeserializeObject<int>(json);
        }
    }
}
