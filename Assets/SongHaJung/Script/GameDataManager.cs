using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class Serialization<T>
{
    //생성자
    public Serialization(List<T> _target) => target = _target;
    //T에 아이템 대입 (리스트로 json파싱이 안되기때문에 클래스로 넣어줘야함)
    public List<T> target;
}
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

    public List<Item> AllItemList, MyItemList, CurItemList, PositionList;
    public string curType = "Building";
    public GameObject[] Slot, UsingImage, AllModels;
    public Image[] TapImage, ItemImage;
    public Sprite[] ItemSprite;
    public GameObject ExplainPanel;
    public GameObject Tag;
    public GameObject wood;
    public MainMenu main;

    public static int selectionIndex;
    //public static float enhance;
    public int fixPosition;
    public int beforeSelect;

    public int buildingWoodMoney;
    //public int allWoodMoney;
    public TextMeshProUGUI showWoodMoney;
    public TextMeshProUGUI checkbu;

    string filepath;
    string filepath2;

    //드래그 이동
    private bool isMouseDragging = false;
    private Vector3 screenPosition;
    private Vector3 offset;
    private float builgdingZ = 0.03f;
    private float frontZ = 0.035f;
    private float backz = 0.045f;
    private bool IsMove;

    //구매하고난뒤 버튼 상태 바꿔주기
    private Button checkbtn;

    public Button[] slotbu;


    //건물위치, 상태값 저장
    public List<BuildingInfo> buildingInfoList = new List<BuildingInfo>();

    private void Start()
    {
       
        checkbtn = ExplainPanel.transform.Find("ClickBuilding").GetComponent<Button>();

        int startIndex = 0;
        //전체 아이템 리스트 불러오기
        string[] line = ItemDataBase.text.Substring(startIndex, ItemDataBase.text.Length - 1).Split("\n");  //마지막 엔터자리 지워주기(엑셀로 작업하면 마지막 엔터자리까지뜨기때문)

        //List에 아이템리스트 삽입
        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split("\t");
            AllItemList.Add(new Item(row[0], row[1], row[2], row[3], row[4], row[5], row[6], row[7] == "TRUE", row[8], row[9], row[10], row[11], row[12], row[13]));
        }

        filepath = Application.persistentDataPath + "/MyItemText.txt";
        filepath2 = Application.persistentDataPath + "/PositionData.txt";

        Load();
    
        PointerClick(0); //기본 첫번째 디테일 메뉴가 뜨도록

    }

    private void Update()
    {
        showWoodMoney.text = GameManager.Instance.goodsManager.wood.ToString();

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
            Debug.Log("Building position :" + AllModels[selectionIndex].transform.position);

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

    //배치눌렀을때
    public void GetItemClick()
    {
        //Item curItem = MyItemList.Find(x => x.Name == Slot.text);
        
        //선택아이템 -> 노랑클릭 true될때
        IsMove = true;
        Item curItem = CurItemList.Find(x => x.isUsing == true);

        if (curItem != null)
        {
            Save();
        }

        selectionIndex = int.Parse(curItem.Index);
        AllModels[selectionIndex].SetActive(true);
        GameManager.Instance.goodsManager.enhance += float.Parse(curItem.SpecOfEnhance);
        buildingWoodMoney = int.Parse(curItem.WoodMoney);

        if (AllModels[selectionIndex].tag == "Finish")
        {
            AllModels[fixPosition].SetActive(false);
            fixPosition = selectionIndex;
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
    //배치완료됐을때
    public void NoDrag()
    {
        // woodmoney 깎이고, 강화정보 전투에 넘겨줘야함
        main.OnClickBuildingBack();
        GameManager.Instance.goodsManager.wood -= buildingWoodMoney;
        selectionIndex = beforeSelect;

        Debug.Log("Enhance: " + GameManager.Instance.goodsManager.enhance);

        IsMove = false;
        Save();
    }

    //배치취소(돌아가기버튼 눌렀을때)
    public void Nobuildbutton()
    {
        main.OnClickBuilding();
        IsMove = false;
        AllModels[beforeSelect].SetActive(true);
        AllModels[selectionIndex].SetActive(false);

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

        Save();
    }

    public void TapClick(string tapName)
    {
        //현재 아이템 리스트에 클릭한 타입만 추가
        curType = tapName;
        CurItemList = AllItemList.FindAll(x => x.Type == tapName);

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
            case "Building": tabNum = 0;  break;
            case "FrontFurniture": tabNum = 1;  break;
            case "BackFurniture": tabNum = 2;  break;

        }
        //if (curType == tapName)
        //{
        //    slotbu[tabNum].interactable = true;
        //}


        // 탭 이미지 교환
        //for (int i = 0; i < slotbu.Length; i++)
        //{
        //    slotbu[i].interactable = i == tabNum ? true : false;
        //}
    }



    //디테일 메뉴와 설명이 뜨도록 로드
    public void PointerClick(int slotNum)
    {
        buildingWoodMoney = int.Parse(CurItemList[slotNum].WoodMoney);

        if(buildingWoodMoney > GameManager.Instance.goodsManager.wood)
        {
            checkbtn.interactable = false;
        }
        else
        {
            checkbtn.interactable = true;
        }

        ExplainPanel.SetActive(true);
        wood.SetActive(true);

        ExplainPanel.GetComponentInChildren<TextMeshProUGUI>().text = CurItemList[slotNum].Explain;
        ExplainPanel.transform.GetChild(2).GetComponentInChildren<Image>().sprite = Slot[slotNum].transform.GetChild(0).GetComponent<Image>().sprite;
        //ExplainPanel.transform.GetChild(2).GetComponentInChildren<Image>().sprite = Slot[slotNum].transform.GetComponent<Image>().sprite; 
        ExplainPanel.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "모든 영웅 체력 및 공격력" + CurItemList[slotNum].SpecOfEnhance + "% 증가";
        ExplainPanel.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "(레벨 당" + CurItemList[slotNum].SpecOfLevel + "% 증가)";
        ExplainPanel.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = CurItemList[slotNum].SpecOfContent;
        wood.GetComponentInChildren<TextMeshProUGUI>().text = CurItemList[slotNum].WoodMoney;

    }

    void Save()
    {
     
        string jdata = JsonUtility.ToJson(new Serialization<Item>(AllItemList));
        File.WriteAllText(filepath, jdata); //text 저장

      
        buildingInfoList.Clear();
        for (var i = 0; i < AllModels.Length; ++i)
        {
            var pos = AllModels[i].transform.position;
            bool active = AllModels[i].activeSelf;
            buildingInfoList.Add(new BuildingInfo(i, pos.x, active));
        }

        string bdata = JsonUtility.ToJson(new Serialization<BuildingInfo>(buildingInfoList));
        File.WriteAllText(filepath2, bdata); //text 저장

        TapClick(curType);
    }

    void Load()
    {
        if(!File.Exists(filepath))
        {
            Save();
            return;
        }
        string jdata = File.ReadAllText(filepath);
        AllItemList = JsonUtility.FromJson<Serialization<Item>>(jdata).target;

        if (File.Exists(filepath2))
        {
            string bdata = File.ReadAllText(filepath2);
            buildingInfoList = JsonUtility.FromJson<Serialization<BuildingInfo>>(bdata).target;
        }
        else
        {
            buildingInfoList.Clear();
            for (int i = 0; i < AllModels.Length; i++)
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