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
    public Item(string _Type, string _Name, string _Explain, string _SpecOfEnhance, string _SpecOfLevel, string _SpecOfConten, string _Number,bool _isUsing)
    {
        Type = _Type;
        Name = _Name;
        Explain = _Explain;
        SpecOfEnhance = _SpecOfEnhance;
        SpecOfLevel = _SpecOfLevel;
        SpecOfConten = _SpecOfConten;
        Number = _Number;
        isUsing = _isUsing;
    }

    public string Type, Name, Explain, SpecOfEnhance, SpecOfLevel, SpecOfConten, Number;
    public bool isUsing;
}
public class GameDataManager : MonoBehaviour
{

    public TextAsset BuildingDataBase;
    public List<Item> AllmyItemList, MyItemList, CurItemList;
    
    public string curType = "Building"; //�޴�â Ű�ڸ��� �⺻ slot �޴��� �����ϱ⶧���� building���� ����
    public GameObject[] Slot, UsingIamge;

    public Image[] tapImage, itemImage;
    
    public Sprite TabIdleSprite, TabSelectSprite;
    public Sprite[] ItemSprite;
    public GameObject ExplainPanel;


    void Start()
    {
        //��ü ������ ����Ʈ �ҷ�����
        //�������۾��߱� ������ ������ �����ڸ� �����ֱ� -> -1
        int startIndex = 0;
        string[] line = BuildingDataBase.text.Substring(startIndex, BuildingDataBase.text.Length - 1).Split("\n");

        for(int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split("\t");
            AllmyItemList.Add(new Item(row[0], row[1], row[2], row[3], row[4], row[5], row[6], row[7] == "TRUE"));
        }
        Load();
        PointerClick(0);
    }

    public void GetItemClick()
    {
        //Item curItem = MyItemList.Find(x => x.Name == Slot.text);
        
        //Item curItem = MyItemList.FindIndex(x => x.Number == CurItemList.Number);
    }
    public void RemoveItemClick()
    {

    }
    public void SlotClick(int slotNum)
    {
        Item CurItem = CurItemList[slotNum];
        //������� �������� ã�Ƽ�CurItemOList ����
        Item UsingItem = CurItemList.Find(x => x.isUsing == true);

        if (curType == "Building")
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
            if (UsingItem !=null)
            {
                UsingItem.isUsing = false;
            }
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
            bool isExist = i < CurItemList.Count;

            Slot[i].SetActive(isExist); // CurItemList������ŭ slot Ȱ��ȭ 

            //Slot[i].GetComponentInChildren<TextMeshProUGUI>().text = i < CurItemList.Count ? CurItemList[i].Name + "/" + CurItemList[i].isUsing:"";

            //�������̹����� ������� �̹��� ���̱�
            if(isExist)
            {
                //AllmyItemList �̸��� CurItemList �̸��� ���ٸ� Itemimgae����
                itemImage[i].sprite = ItemSprite[AllmyItemList.FindIndex(x => x.Name == CurItemList[i].Name)];

                UsingIamge[i].SetActive(CurItemList[i].isUsing);
            }
        }

        //���̹���

        int tabNum = 0;

        switch (tapName)
        {
            case "Building": tabNum = 0; break;
            case "FrontFurniture": tabNum = 1; break;
            case "BackFurniture": tabNum = 2; break;
        }
        //���̹���
        //for (int i = 0; i < tapImage.Length; i++)
        //{
        //    tapImage[i].sprite = i == tabNum ? TabSelectSprite : TabIdleSprite;
        //}

    }
    public void PointerClick(int slotNum)
    {
        ExplainPanel.SetActive(true);
        ExplainPanel.GetComponentInChildren<TextMeshProUGUI>().text = CurItemList[slotNum].Explain;
        ExplainPanel.transform.GetChild(2).GetComponentInChildren<Image>().sprite = Slot[slotNum].transform.GetChild(0).GetComponent<Image>().sprite;
        //ExplainPanel.transform.GetChild(2).GetComponentInChildren<Image>().sprite = Slot[slotNum].transform.GetComponent<Image>().sprite; 
        ExplainPanel.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = CurItemList[slotNum].SpecOfEnhance;
        ExplainPanel.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = CurItemList[slotNum].SpecOfLevel;
        ExplainPanel.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = CurItemList[slotNum].SpecOfConten;
       
    }

    //public void PointerUp(int slotNum)
    //{
       
    //    ExplainPanel.SetActive(false);
    //}
    void Save()
    {
        string jdata = JsonConvert.SerializeObject(MyItemList); //AllmyList�� List�����̱� ������ SerializeObject�� ����ȭ -> ���ٷ� ����

        //�������� ����
        File.WriteAllText(Application.dataPath + "/SongHaJung/BuildingData.txt", jdata); //text ����

        TapClick(curType);
    }


    //Load�� Save�� �ݴ� ������ ����
    void Load()
    {
        string jdata = File.ReadAllText(Application.dataPath + "/SongHaJung/BuildingData.txt");
        MyItemList = JsonConvert.DeserializeObject<List<Item>>(jdata); // ������ȭ -> Json������ list�������� ����

        TapClick(curType);
    }
}
