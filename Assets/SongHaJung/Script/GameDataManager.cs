using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using TMPro;

[System.Serializable]
public class Item
{
    //������
    public Item(string _Type, string _Name, string _Explain, string _Number,bool _isUsing)
    {
        Type = _Type;
        Name = _Name;
        Explain = _Explain;
        Number = _Number;
        isUsing = _isUsing;
    }

    public string Type, Name, Explain, Number;
    public bool isUsing;
}
public class GameDataManager : MonoBehaviour
{

    public TextAsset ItemDatabase;
    public List<Item> AllmyItemList, MyItemList, CurItemList;
    
    public string curType = "Building"; //�޴�â Ű�ڸ��� �⺻ slot �޴��� �����ϱ⶧���� building���� ����
    public GameObject[] Slot;

    

    public Sprite TabIdleSprite, TabSelectSprite;


    void Start()
    {
        //��ü ������ ����Ʈ �ҷ�����
        //�������۾��߱� ������ ������ �����ڸ� �����ֱ� -> -1
        string[] line = ItemDatabase.text.Substring(0, ItemDatabase.text.Length - 1).Split("\n");

        for(int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split("\t");
            AllmyItemList.Add(new Item(row[0], row[1], row[2], row[3], row[4] == "TRUE"));
        }
        Load();
    }
    public void SlotClick(int slotNum)
    {
        Item CurItem = CurItemList[slotNum];
        Item UsingItem = CurItemList.Find(x => x.isUsing == true);

        if (curType == "Building")
        {
            CurItem.isUsing = true;

        }
        else
        {

        }
        Save();
    }

    public void TapClick(string tapName)
    {
        //���� ������ ����Ʈ�� Ŭ���� Ÿ�Ը� �߰�
        curType = tapName;
        CurItemList = MyItemList.FindAll(x => x.Type == tapName); //tapName�� ���� ���� �����ͼ� CurItemList �־���

        //���԰� �ؽ�Ʈ ���̱�
        for (int i = 0; i < Slot.Length; i++) 
        {
            Slot[i].SetActive(i < CurItemList.Count); // CurItemList������ŭ slot Ȱ��ȭ 

            Slot[i].GetComponentInChildren<TextMeshProUGUI>().text = i < CurItemList.Count ? CurItemList[i].Name + "/" + CurItemList[i].isUsing:"";
        }

        int tabNum = 0;

        switch(tapName)
        {
            case "Building": tabNum = 0; break;
            case "FrontFurniture": tabNum = 1; break;
            case "BackFurniture": tabNum = 2; break;
        }
        //for(int i=0; i< tapSprites.Length;i++)
        //{
        //    tapSprites[i].sprite = i
        //}
        
    }
    void Save()
    {
        string jdata = JsonConvert.SerializeObject(AllmyItemList); //AllmyList�� List�̱� ������ SerializeObject�� ����ȭ -> ���ٷ� ����
        print(jdata);

        //�������� ����
        File.WriteAllText(Application.dataPath + "/SongHaJung/BuildingData.txt", jdata); //text ����
    }


    //Load�� Save�� �ݴ� ������ ����
    void Load()
    {
        string jdata = File.ReadAllText(Application.dataPath + "/SongHaJung/BuildingData.txt");
        MyItemList = JsonConvert.DeserializeObject<List<Item>>(jdata); // ������ȭ -> list��������

        TapClick(curType);
    }
}
