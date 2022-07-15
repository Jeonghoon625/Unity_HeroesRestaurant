using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using TMPro;

[System.Serializable]
public class Item
{
    //생성자
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
    
    public string curType = "Building"; //메뉴창 키자마자 기본 slot 메뉴가 떠야하기때문에 building으로 설정
    public GameObject[] Slot;

    

    public Sprite TabIdleSprite, TabSelectSprite;


    void Start()
    {
        //전체 아이템 리스트 불러오기
        //엑셀로작업했기 때문에 마지막 엔터자리 없애주기 -> -1
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
        //현재 아이템 리스트에 클릭한 타입만 추가
        curType = tapName;
        CurItemList = MyItemList.FindAll(x => x.Type == tapName); //tapName의 값을 전부 가져와서 CurItemList 넣어줌

        //슬롯과 텍스트 보이기
        for (int i = 0; i < Slot.Length; i++) 
        {
            Slot[i].SetActive(i < CurItemList.Count); // CurItemList갯수만큼 slot 활성화 

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
        string jdata = JsonConvert.SerializeObject(AllmyItemList); //AllmyList가 List이기 때문에 SerializeObject로 직렬화 -> 한줄로 나옴
        print(jdata);

        //내폴더에 저장
        File.WriteAllText(Application.dataPath + "/SongHaJung/BuildingData.txt", jdata); //text 저장
    }


    //Load는 Save와 반대 구조로 구현
    void Load()
    {
        string jdata = File.ReadAllText(Application.dataPath + "/SongHaJung/BuildingData.txt");
        MyItemList = JsonConvert.DeserializeObject<List<Item>>(jdata); // 역직렬화 -> list형식으로

        TapClick(curType);
    }
}
