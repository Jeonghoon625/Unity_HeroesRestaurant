using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class BuildingInfo
{
    public BuildingInfo(int _activIndex, float _positionX, bool _Istrue)
    {
        activIndex = _activIndex;
        positionX = _positionX;
        Istrue = _Istrue;
    }
    public int activIndex;
    public float positionX;
    public bool Istrue;
}

[System.Serializable]
public class Item
{
    //생성자
    public Item(string _Type, string _Name, string _Explain, string _SpecOfEnhance, string _SpecOfLevel, string _SpecOfContent, string _WoodMoney, bool _isUsing, string _Index, string _FloatX, string _FloatY, string _FloatZ, string _UsingChange, string _PositionChange)
    {
        Type = _Type;
        Name = _Name;
        Explain = _Explain;
        SpecOfEnhance = _SpecOfEnhance;
        SpecOfLevel = _SpecOfLevel;
        SpecOfContent = _SpecOfContent;
        WoodMoney = _WoodMoney;
        isUsing = _isUsing;
        Index = _Index;
        FloatX = _FloatX;
        FloatY = _FloatY;
        FloatZ = _FloatZ;
        UsingChange = _UsingChange;
        PositionChange = _PositionChange;
    }

    public string Type, Name, Explain, SpecOfEnhance, SpecOfLevel, SpecOfContent, WoodMoney, Index, FloatX, FloatY, FloatZ, UsingChange, PositionChange;
    public bool isUsing;
}

public class GameDataManager : MonoBehaviour
{
    public TextAsset ItemDataBase;

    public List<Item> AllItemList, MyItemList, CurItemList;
    public string curType = "Building";
    public GameObject[] Slot, UsingImage, AllModels;
    public Image[] TapImage, ItemImage;
    public Sprite[] ItemSprite;
    public GameObject ExplainPanel;
    public MainMenu main;

    public static int selectionIndex = 0;
    public static int testIndex = 0;


    //드래그 이동
    private bool isMouseDragging = false;
    private Vector3 screenPosition;
    private Vector3 offset;
    private float builgdingZ = 0.03f;
    private float frontZ = 0.035f;
    private float backz = 0.045f;
    private bool IsMove;


    //건물위치, 상태값 저장
    public List<BuildingInfo> buildingInfoList = new List<BuildingInfo>();
    private void Start()
    {
        int startIndex = 0;
        //전체 아이템 리스트 불러오기
        string[] line = ItemDataBase.text.Substring(startIndex, ItemDataBase.text.Length - 1).Split("\n");  //마지막 엔터자리 지워주기(엑셀로 작업하면 마지막 엔터자리까지뜨기때문)
     
        //List에 아이템리스트 삽입
        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split("\t");
            AllItemList.Add(new Item(row[0], row[1], row[2], row[3], row[4], row[5], row[6], row[7] == "TRUE", row[8], row[9], row[10], row[11], row[12], row[13]));
        }
        Load();
        PointerClick(0); //기본 첫번째 디테일 메뉴가 뜨도록
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (IsMove)
            {
                isMouseDragging = true;

                screenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                offset = AllModels[selectionIndex].transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z));
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            isMouseDragging = false;
        }
        if (isMouseDragging)
        {
            float z = builgdingZ;
            float x = Camera.main.transform.position.x;

            if (selectionIndex < 18)
            {
                isMouseDragging = false;
                x = 0;
            }
            else if (selectionIndex > 17 && selectionIndex < 38) z = frontZ;
            else z = backz;

            Vector3 currentScreenSpace = new Vector3(x, 0f, z);
            AllModels[selectionIndex].transform.position = currentScreenSpace;
        }
    }

    //첫번째 배치눌렀을때
    public void GetItemClick()
    {
        //선택아이템 -> 노랑클릭 true될때
        IsMove = true;
        Item curItem = CurItemList.Find(x => x.isUsing == true);

        Item curPosition = CurItemList.Find(x => x.FloatX=="0");

        if (curItem != null|| curPosition != null)
        {
            Save();
        }

        selectionIndex = int.Parse(curItem.Index);
        AllModels[selectionIndex].SetActive(true);

        //Finish tag = mani building setactive false 
        if (AllModels[selectionIndex].tag == "Finish")
        {
            AllModels[testIndex].SetActive(false);
            testIndex = selectionIndex;
        }

        if (AllModels[selectionIndex] != null)
        {
            float z = 0;

            if (selectionIndex < 18) z = builgdingZ;
            else if (selectionIndex > 17 && selectionIndex < 38) z = frontZ;
            else z = backz;

            AllModels[selectionIndex].transform.position = new Vector3(0, 0, z);

        }
        main.BuildingOnMain(); 
    }
    public void ChangeOnMainModels()
    {
        if (selectionIndex < 18)
        {
            AllModels[selectionIndex].SetActive(false);
        }

        if (selectionIndex > 17 && selectionIndex < 58)
        {
            AllModels[selectionIndex].SetActive(true);
        }
    }

    //최종 배치눌렀을때  
    public void NoDragForSave()
    {
        main.OnClickBuildingBack();
        IsMove = false;
        Save();
    }

    public void SlotClick(int slotNum)
    {
        Item CurItem = CurItemList[slotNum];
        Item UsingItem = CurItemList.Find(x => x.isUsing == true);

        CurItem.isUsing = !CurItem.isUsing;
        if (UsingItem != null)
        {
            UsingItem.isUsing = false;
        }

        Debug.Log(CurItem.Index);
        Save();
    }

    public void TapClick(string tapName)
    {
        //현재 아이템 리스트에 클릭한 타입만 추가
        curType = tapName;
        CurItemList = MyItemList.FindAll(x => x.Type == tapName);

        //아이템 이미지와 사용중인지 뜨도록
        for (int i = 0; i < Slot.Length; i++)
        {
            //슬롯과 텍스트 보이기
            bool isExist = i < CurItemList.Count;
            Slot[i].SetActive(isExist);

            if (isExist)
            {
                ItemImage[i].sprite = ItemSprite[AllItemList.FindIndex(x => x.Name == CurItemList[i].Name)];
                UsingImage[i].SetActive(CurItemList[i].isUsing);

            }
        }
        //탭이미지
        int tabNum = 0;

        switch (tapName)
        {
            case "Building": tabNum = 0; break;
            case "FrontFurniture": tabNum = 1; break;
            case "BackFurniture": tabNum = 2; break;
        }

    }

    //디테일 메뉴와 설명이 뜨도록 로드
    public void PointerClick(int slotNum)
    {
        ExplainPanel.SetActive(true);

        ExplainPanel.GetComponentInChildren<TextMeshProUGUI>().text = CurItemList[slotNum].Explain;
        ExplainPanel.transform.GetChild(2).GetComponentInChildren<Image>().sprite = Slot[slotNum].transform.GetChild(0).GetComponent<Image>().sprite;
        //ExplainPanel.transform.GetChild(2).GetComponentInChildren<Image>().sprite = Slot[slotNum].transform.GetComponent<Image>().sprite; 
        ExplainPanel.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = CurItemList[slotNum].SpecOfEnhance;
        ExplainPanel.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = CurItemList[slotNum].SpecOfLevel;
        ExplainPanel.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = CurItemList[slotNum].SpecOfContent;

    }

    void Save()
    {
        string jdata = JsonConvert.SerializeObject(MyItemList);
        File.WriteAllText(Application.dataPath + "/SongHaJung/BuildingData.txt", jdata); //text 저장

        for (var i = 0; i < buildingInfoList.Count; ++i)
        {
            // Load와 반대로 
            AllModels[i].SetActive(buildingInfoList[i].Istrue);
            var pos = AllModels[i].transform.position;
            pos.x = buildingInfoList[i].positionX;
            AllModels[i].transform.position = pos;
        }

        string bdata = JsonConvert.SerializeObject(buildingInfoList);
        File.WriteAllText(Application.dataPath + "/SongHaJung/PositionData.txt", bdata);

        TapClick(curType);
    }


    void Load()
    {
        string jdata = File.ReadAllText(Application.dataPath + "/SongHaJung/BuildingData.txt");
        MyItemList = JsonConvert.DeserializeObject<List<Item>>(jdata);

        var path = Application.dataPath + "/SongHaJung/PositionData.txt";
        if (File.Exists(path))
        {
            string bdata = File.ReadAllText(Application.dataPath + "/SongHaJung/PositionData.txt");
            buildingInfoList = JsonConvert.DeserializeObject<List<BuildingInfo>>(bdata);
        }
        else
        {
            int finalIndex = 57;
            for (int i = 0; i <= finalIndex; i++)
            {
                buildingInfoList.Add(new BuildingInfo(i, 0f, false));
            }
            buildingInfoList[0].Istrue = true;
        }
        TapClick(curType);

        for (var i = 0; i < buildingInfoList.Count; ++i)
        {
            AllModels[i].SetActive(buildingInfoList[i].Istrue);
            var pos = AllModels[i].transform.position;
            pos.x = buildingInfoList[i].positionX;
            AllModels[i].transform.position = pos;
        }
    }
}
