using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using TMPro;
using UnityEngine.UI;


[System.Serializable]
public class Item
{
    //생성자
    public Item(string _Type, string _Name, string _Explain, string _SpecOfEnhance, string _SpecOfLevel, string _SpecOfConten, string _Number, bool _isUsing, string _Index)
    {
        Type = _Type;
        Name = _Name;
        Explain = _Explain;
        SpecOfEnhance = _SpecOfEnhance;
        SpecOfLevel = _SpecOfLevel;
        SpecOfConten = _SpecOfConten;
        Number = _Number;
        isUsing = _isUsing;
        Index = _Index;
    }

    public string Type, Name, Explain, SpecOfEnhance, SpecOfLevel, SpecOfConten, Number, Index;
    
    public bool isUsing;
}
public class GameDataManager : MonoBehaviour
{
    public TextAsset ItemDataBase;
    public List<Item> AllItemList, MyItemList, CurItemList;
    public string curType = "Building";
    public GameObject[] Slot, UsingImage;
    public Image[] TapImage, ItemImage;
    public Sprite[] ItemSprite;
    public GameObject ExplainPanel;
    public MainMenu main;

    

    public ModelsCreation modelContainer;

    public static int selectionIndex;

  

    private void Start()
    {
        int startIndex = 0;
        //전체 아이템 리스트 불러오기
        string[] line = ItemDataBase.text.Substring(startIndex, ItemDataBase.text.Length - 1).Split("\n");
        for (int i = 0; i < line.Length; i++) 
        {
            string[] row = line[i].Split("\t");
            AllItemList.Add(new Item(row[0], row[1], row[2], row[3], row[4], row[5], row[6], row[7] == "TRUE", row[8]));
        }
        
        Load();
        PointerClick(0); //기본 디테일 메뉴가 뜨도록
        ExplainPanel.SetActive(true);
     


    }

    //배치눌렀을때
    public void GetItemClick()
    {
        //Item curItem = MyItemList.Find(x => x.Name == Slot.text);
        Item curItem = CurItemList.Find(x => x.isUsing == true);

        if (curItem != null)
        {

            CurItemList.Sort((p1, p2) => p1.Index.CompareTo(p2.Index));
            Save();
        }

        selectionIndex = int.Parse(curItem.Index);
        ShowItemInMain();

    }

    public void ShowItemInMain()
    {
        main.BuildingOnMain();
        modelContainer.Update();


        Save();
    }

    public void EndBuilding()
    {
        
        Load();
        
    }

    //public void RemoveItemClick()
    //{
    //    Item curItem = MyItemList.Find(x => x.isUsing == true);
    //    if (curItem != null)
    //    {
    //        MyItemList.Remove(curItem);
    //    }
    //    MyItemList.Sort((p1, p2) => p1.Index.CompareTo(p2.Index));
    //    Save();
    //}

    public void SlotClick(int slotNum)
    {
        Item CurItem = CurItemList[slotNum];
        Item UsingItem = CurItemList.Find(x => x.isUsing == true);

        if(curType== "Building")
        {
            if(UsingItem != null)
            {
                UsingItem.isUsing = false;
                CurItem.isUsing = true;
            }
           
        }
        else
        {
            CurItem.isUsing = !CurItem.isUsing;
            if(UsingItem != null)
            {
                UsingItem.isUsing = false;
            }
        }
        Save();
    }
    public void TapClick(string tapName)
    {
        //현재 아이템 리스트에 클릭한 타입만 추가
        curType = tapName;
        CurItemList = MyItemList.FindAll(x => x.Type == tapName);

        //아이템 이미지와 사용중인지 뜨도록
        for(int i=0;i<Slot.Length;i++)
        {
            //슬롯과 텍스트 보이기
            bool isExist = i < CurItemList.Count;
            Slot[i].SetActive(isExist);

            if(isExist)
            {
                ItemImage[i].sprite = ItemSprite[AllItemList.FindIndex(x => x.Name == CurItemList[i].Name)];
                UsingImage[i].SetActive(CurItemList[i].isUsing);
            }
        }
        //탭이미지
        int tabNum = 0;
        switch(tapName)
        {
            case "Building" :tabNum = 0;break;
            case "FrontFurniture": tabNum = 1;break;
            case "BackFurniture": tabNum = 2;break;
        }
        
        //for (int i = 0; i < TapImage.Length; i++)
        //{
        //    TapImage[i].sprite = i == tabNum ? TapSelectSprite : TabIdleSprite;
        //}
    }
    public void PointerClick(int slotNum)
    {
       
        ExplainPanel.GetComponentInChildren<TextMeshProUGUI>().text = CurItemList[slotNum].Explain;
        ExplainPanel.transform.GetChild(2).GetComponentInChildren<Image>().sprite = Slot[slotNum].transform.GetChild(0).GetComponent<Image>().sprite;
        //ExplainPanel.transform.GetChild(2).GetComponentInChildren<Image>().sprite = Slot[slotNum].transform.GetComponent<Image>().sprite; 
        ExplainPanel.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = CurItemList[slotNum].SpecOfEnhance;
        ExplainPanel.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = CurItemList[slotNum].SpecOfLevel;
        ExplainPanel.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = CurItemList[slotNum].SpecOfConten;

    }

    void Save()
    { 
        string jdata = JsonConvert.SerializeObject(MyItemList); 
        File.WriteAllText(Application.dataPath + "/SongHaJung/BuildingData.txt", jdata); //text 저장

        TapClick(curType);
    }
    void Load()
    {
        string jdata = File.ReadAllText(Application.dataPath + "/SongHaJung/BuildingData.txt");
        MyItemList = JsonConvert.DeserializeObject<List<Item>>(jdata);

        TapClick(curType);
    }
}
