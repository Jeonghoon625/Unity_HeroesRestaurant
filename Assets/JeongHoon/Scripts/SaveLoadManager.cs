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

    public void SaveTime()
    {
        Debug.Log("�ð� ����");
        string fileName = "Time";
        string path = Application.dataPath + fileName + ".Json";

        var setJson = JsonConvert.SerializeObject(System.DateTime.Now);
        File.WriteAllText(path, setJson);
    }

    public System.DateTime LoadTime()
    {
        Debug.Log("�ð� �ε�");

        string fileName = "Time";
        string path = Application.dataPath + fileName + ".Json";

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


    public void GenerateDefaultCurrencyReserve()
    {
        Debug.Log($"�ʱ� ��� ������ ������ ����");

        //Currency ������
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
        Debug.Log($"�ʱ� ���� ������ ������ ����");

        //Food ������
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
        Debug.Log("���� ������ �ε�");

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
        Debug.Log("��� ������ �ε�");

        string fileName = "CurrencyReserve";
        string path = Application.dataPath + fileName + ".Json";

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
        //Food ������
        string fileName = "FoodReserve";
        string path = Application.dataPath + fileName + ".Json";

        var setJson = JsonConvert.SerializeObject(GameManager.Instance.goodsManager.foodReserve);
        File.WriteAllText(path, setJson);
    }

    public void SaveCurrencyReserve()
    {
        //Currency ������
        string fileName = "CurrencyReserve";
        string path = Application.dataPath + fileName + ".Json";

        var setJson = JsonConvert.SerializeObject(GameManager.Instance.goodsManager.currencyReserve);
        File.WriteAllText(path, setJson);
    }
}
