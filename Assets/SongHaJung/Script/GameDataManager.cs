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
    //������
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



    private void Start()
    {


        int startIndex = 0;
        //��ü ������ ����Ʈ �ҷ�����
        string[] line = ItemDataBase.text.Substring(startIndex, ItemDataBase.text.Length - 1).Split("\n");  //������ �����ڸ� �����ֱ�(������ �۾��ϸ� ������ �����ڸ������߱⶧��)


        //List�� �����۸���Ʈ ����
        for (int i = 0; i < line.Length; i++) 
        {
            string[] row = line[i].Split("\t");
            AllItemList.Add(new Item(row[0], row[1], row[2], row[3], row[4], row[5], row[6], row[7] == "TRUE", row[8], row[9], row[10], row[11], row[12], row[13]));
        }
        
        Load();

        //PointerClick(0); //�⺻ ������ �޴��� �ߵ���

    }
    private bool isMouseDragging;
    private Vector3 screenPosition;
    private Vector3 offset;

    private void Update()
    {
        float moveX = 0f;

        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("������?");
        }
        Debug.Log("�Դ�?");
        if (Input.GetMouseButtonDown(0))
        {

            if (AllModels[selectionIndex] != null)
            {
                isMouseDragging = true;
                Debug.Log("our target position :" + AllModels[selectionIndex].transform.position);
                //Here we Convert world position to screen position.
                screenPosition = Camera.main.WorldToScreenPoint(AllModels[selectionIndex].transform.position);
                offset = AllModels[selectionIndex].transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 0.02f, -0.1f));
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isMouseDragging = false;
        }

        if (isMouseDragging)
        {
            //tracking mouse position.
            Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, 0.02f, -0.1f);

            //convert screen position to world position with offset changes.
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenSpace) + offset;

            //It will update target gameobject's current postion.
            AllModels[selectionIndex].transform.position = currentPosition;
        }

    }

    //��ġ��������
    public void GetItemClick()
    {

        //Item curItem = MyItemList.Find(x => x.Name == Slot.text);

        //���þ����� -> ���Ŭ�� true�ɶ�
        Item curItem = CurItemList.Find(x => x.isUsing == true);


        if (curItem != null)
        {
            //CurItemList.Sort((p1, p2) => p1.Index.CompareTo(p2.Index));
            
            Save();
        }

        selectionIndex = int.Parse(curItem.Index);
        AllModels[selectionIndex].SetActive(true);
        AllModels[selectionIndex].transform.position = new Vector3(0, 0, 0);

      
            ShowItemInMain();
    }
    

    public void ShowItemInMain()
    {
        
        main.BuildingOnMain();

        


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

        //if(curType== "Building")
        //{
        //    if(UsingItem != null)
        //    {
        //        UsingItem.isUsing = false;
        //        CurItem.isUsing = true;
        //    }
           
        //}
        //else
        //{
        //    CurItem.isUsing = !CurItem.isUsing;
        //    if(UsingItem != null)
        //    {
        //        UsingItem.isUsing = false;
        //    }
        //} 
        
        CurItem.isUsing = !CurItem.isUsing;
        if(UsingItem != null)
        {
            UsingItem.isUsing = false;
        }
        
        Debug.Log(CurItem.Index);
        Save();
    }
    public void TapClick(string tapName)
    {
        //���� ������ ����Ʈ�� Ŭ���� Ÿ�Ը� �߰�
        curType = tapName;
        CurItemList = MyItemList.FindAll(x => x.Type == tapName);

        //������ �̹����� ��������� �ߵ���
        for (int i = 0; i < Slot.Length; i++) 
        {
            //���԰� �ؽ�Ʈ ���̱�
            bool isExist = i < CurItemList.Count;
            Slot[i].SetActive(isExist);

            if(isExist)
            {
                ItemImage[i].sprite = ItemSprite[AllItemList.FindIndex(x => x.Name == CurItemList[i].Name)];
                UsingImage[i].SetActive(CurItemList[i].isUsing);
                
            }
        }
        //���̹���
        int tabNum = 0;
        switch(tapName)
        {
            case "Building" :tabNum = 0;break;
            case "FrontFurniture": tabNum = 1;break;
            case "BackFurniture": tabNum = 2;break;
        }
        
        // �� �̹��� ��ȯ
        //for (int i = 0; i < TapImage.Length; i++)
        //{
        //    TapImage[i].sprite = i == tabNum ? TapSelectSprite : TabIdleSprite;
        //}
    }

    //������ �޴��� ������ �ߵ��� �ε�
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
        File.WriteAllText(Application.dataPath + "/SongHaJung/BuildingData.txt", jdata); //text ����

        TapClick(curType);
    }
    void Load()
    {
        string jdata = File.ReadAllText(Application.dataPath + "/SongHaJung/BuildingData.txt");
        MyItemList = JsonConvert.DeserializeObject<List<Item>>(jdata);

        TapClick(curType);
    }
}
