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
        //Debug.Log("SaveData");
        SaveReserve();
        SaveGold();
        SaveWood();
        SaveStageInfo();
        SaveStamina();
        SaveMaxStamina();
        SaveCoolTimeStamina();
        SaveTime();
    }

    public void Load()
    {
        LoadReserve();
        LoadGold();
        LoadWood();
        LoadStageInfo();
        LoadStamina();
        LoadMaxStamina();
        LoadCoolTimeStamina();
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

    public void SaveCoolTimeStamina()
    {
        //Debug.Log("���¹̳� ��Ÿ�� ���� ����");
        string fileName = "CoolTimeStamina";
        string path = Application.persistentDataPath + "/" + fileName + ".Json";

        var setJson = JsonConvert.SerializeObject(GameManager.Instance.goodsManager.coolTimeStaminaLV);
        File.WriteAllText(path, setJson);
    }

    public void LoadCoolTimeStamina()
    {
        //Debug.Log("���¹̳� ��Ÿ�� ���� �ε�");

        string fileName = "CoolTimeStamina";
        string path = Application.persistentDataPath + "/" + fileName + ".Json";

        fileInfo = new FileInfo(path);

        if (!fileInfo.Exists)
        {
            GameManager.Instance.goodsManager.coolTimeStaminaLV = 1;
            SaveCoolTimeStamina();
        }
        else
        {
            string json = File.ReadAllText(path);
            GameManager.Instance.goodsManager.coolTimeStaminaLV = JsonConvert.DeserializeObject<int>(json);
        }
    }

    public void SaveMaxStamina()
    {
        //Debug.Log("���¹̳� �ִ� ������ ���� ����");
        string fileName = "MaxStamina";
        string path = Application.persistentDataPath + "/" + fileName + ".Json";

        var setJson = JsonConvert.SerializeObject(GameManager.Instance.goodsManager.maxStaminaLV);
        File.WriteAllText(path, setJson);
    }

    public void LoadMaxStamina()
    {
        //Debug.Log("���¹̳� �ִ� ������ ���� �ε�");

        string fileName = "MaxStamina";
        string path = Application.persistentDataPath + "/" + fileName + ".Json";

        fileInfo = new FileInfo(path);

        if (!fileInfo.Exists)
        {
            GameManager.Instance.goodsManager.maxStaminaLV = 1;
            SaveMaxStamina();
        }
        else
        {
            string json = File.ReadAllText(path);
            GameManager.Instance.goodsManager.maxStaminaLV = JsonConvert.DeserializeObject<int>(json);
        }
    }

    public void SaveStamina()
    {
        //Debug.Log("���¹̳� ����");
        string fileName = "Stamina";
        string path = Application.persistentDataPath + "/" + fileName + ".Json";

        var setJson = JsonConvert.SerializeObject(GameManager.Instance.goodsManager.stamina);
        File.WriteAllText(path, setJson);
    }

    public void LoadStamina()
    {
        //Debug.Log("���¹̳� �ε�");

        string fileName = "Stamina";
        string path = Application.persistentDataPath + "/" + fileName + ".Json";

        fileInfo = new FileInfo(path);

        if (!fileInfo.Exists)
        {
            GameManager.Instance.goodsManager.stamina = 4;
            SaveStamina();
        }
        else
        {
            string json = File.ReadAllText(path);
            GameManager.Instance.goodsManager.stamina = JsonConvert.DeserializeObject<int>(json);
        }
    }

    public void SaveTime()
    {
        //Debug.Log("�ð� ����");
        string fileName = "Time";
        string path = Application.persistentDataPath + "/" + fileName + ".Json";

        var setJson = JsonConvert.SerializeObject(System.DateTime.Now);
        File.WriteAllText(path, setJson);
    }

    public System.DateTime LoadTime()
    {
       // Debug.Log("�ð� �ε�");

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

 
    public void GenerateDefaultCurrencyReserve()
    {
        //Debug.Log($"�ʱ� ��� ������ ������ ����");

        //Currency ������
        string fileName = "CurrencyReserve";
        string path = Application.persistentDataPath + "/" + fileName + ".Json";

        for (var i = 0; i < GameManager.Instance.resourceManager.currencyData.Count; i++)
        {
            int currencyId = i;

            GameManager.Instance.goodsManager.currencyReserve.Insert(currencyId, 333);
        }

        var setJson = JsonConvert.SerializeObject(GameManager.Instance.goodsManager.currencyReserve);
        File.WriteAllText(path, setJson);
    }

    public void GenerateDefaultFoodReserve()
    {
        //Debug.Log($"�ʱ� ���� ������ ������ ����");

        //Food ������
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
        //Debug.Log("���� ������ �ε�");

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
       // Debug.Log("��� ������ �ε�");

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
        //Food ������
        string fileName = "FoodReserve";
        string path = Application.persistentDataPath + "/" + fileName + ".Json";

        var setJson = JsonConvert.SerializeObject(GameManager.Instance.goodsManager.foodReserve);
        File.WriteAllText(path, setJson);
    }

    public void SaveCurrencyReserve()
    {
        //Currency ������
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
            GameManager.Instance.goodsManager.gold = 10000;
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

    /*
     public void GenerateDefaultProduceTime()
     {
         Debug.Log($"�ʱ� ���� �ð� ����");

         //Currency ������
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

}
