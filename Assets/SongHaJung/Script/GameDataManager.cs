using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class Serialization<T>
{
    //������
    public Serialization(List<T> _target) => target = _target;
    //T�� ������ ���� (����Ʈ�� json�Ľ��� �ȵǱ⶧���� Ŭ������ �־������)
    public List<T> target;
}
[System.Serializable]
public class BuildingInfo
{
    //BuildingInfo(ModelsInfo.modelIndex, pos.x, ModelsInfo.curModelNum, ModelsInfo.buyModelNum
    public BuildingInfo(int _activIndex, Vector3 _positionX)
    {
        activIndex = _activIndex;
        position = _positionX;
        //curNum = _curNum;
        //buyNum = _buyNum;
    }
    public int activIndex;
    public Vector3 position;

}

[System.Serializable]
public class Item
{
    //������
    public Item(string _Type, string _buildNum, string _Explain, string _SpecOfEnhance, string _SpecOfLevel, string _SpecOfContent, string _WoodMoney, bool _isUsing, string _Index, string _FloatX, string _FloatY, string _FloatZ, string _UsingChange, string _PositionChange)
    {
        Type = _Type;
        buildNum = _buildNum;
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

    public string Type, buildNum, Explain, SpecOfEnhance, SpecOfLevel, SpecOfContent, WoodMoney, Index, FloatX, FloatY, FloatZ, UsingChange, PositionChange;
    public bool isUsing;
}

public class GameDataManager : MonoBehaviour
{
    public TextAsset ItemDataBase;

    public List<Item> AllItemList, CurItemList;
    public string curType = "Building";
    public GameObject[] Slot, UsingImage, AllModels;
    public Image[] ItemImage;
    public Sprite[] ItemSprite;
    public GameObject ExplainPanel;
    public GameObject wood;
    public MainMenu main;
    public float enhance;

    public static int selectionIndex;
    public int fixPosition;
    public int beforeSelect;

    public int buildingWoodMoney;
    public TextMeshProUGUI showWoodMoney;

    string filepath;
    string filepath2;

    //�巡�� �̵�
    private bool isMouseDragging = false;
    private Vector3 screenPosition;
    private Vector3 offset;
    private float builgdingZ = 0.03f;
    private float frontZ = 0.035f;
    private float backz = 0.045f;
    private bool IsMove;

    //�����ϰ��� ��ư ���� �ٲ��ֱ�
    private Button checkbtn;
    public Button[] slotbu;


    //�ǹ���ġ, ���°� ����
    public List<BuildingInfo> buildingInfoList = new List<BuildingInfo>();
    public List<int> curbuildNum = new List<int>();
    public List<int> buybuildNum = new List<int>();

    private Vector3 currentScreenSpace;
    private List<GameObject> buildList = new List<GameObject>();
    private GameObject prevCook;
    private void Start()
    {
        //prevCook ��ġ�� finish ��ũ ���� ���� ���� �־���ߵ� 
        
        checkbtn = ExplainPanel.transform.Find("ClickBuilding").GetComponent<Button>();

        int startIndex = 0;
        //��ü ������ ����Ʈ �ҷ�����
        string[] line = ItemDataBase.text.Substring(startIndex, ItemDataBase.text.Length - 1).Split("\n");  //������ �����ڸ� �����ֱ�(������ �۾��ϸ� ������ �����ڸ������߱⶧��)

        //List�� �����۸���Ʈ ����
        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split("\t");
            AllItemList.Add(new Item(row[0], row[1], row[2], row[3], row[4], row[5], row[6], row[7] == "TRUE", row[8], row[9], row[10], row[11], row[12], row[13]));
        }

        filepath = Application.persistentDataPath + "/MyItemText.txt";
        filepath2 = Application.persistentDataPath + "/PositionData.txt";
        Debug.Log(filepath);
   
        Load();

        PointerClick(0); //�⺻ ù��° ������ �޴��� �ߵ���

        for (var i = 0; i < AllModels.Length; ++i)
        {
            if (AllModels[i].tag == "Finish" && AllModels[i].activeSelf == true)
            {
                fixPosition = i;
                break;
            }
        }
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

            currentScreenSpace = new Vector3(x, 0f, z);
            AllModels[selectionIndex].transform.position = currentScreenSpace;
        }
    }

    //��ġ��������
    public void GetItemClick()
    {
        //Item curItem = MyItemList.Find(x => x.Name == Slot.text);

        //���þ����� -> ���Ŭ�� true�ɶ�
        IsMove = true;
        Item curItem = CurItemList.Find(x => x.isUsing == true);
        if (curItem != null)
        {
            //Save();
        }
        selectionIndex = int.Parse(curItem.Index);
        AllModels[selectionIndex].SetActive(true);
        buildingWoodMoney = int.Parse(curItem.WoodMoney);
        enhance = float.Parse(curItem.SpecOfEnhance);

        if (AllModels[selectionIndex].tag == "Finish")
        {
            AllModels[fixPosition].SetActive(false);
            fixPosition = selectionIndex;
            foreach (var i in buildList)
            {
                if (i.tag == "Finish")
                {
                    prevCook = i;
                    i.SetActive(false);
                    break;
                }
            }
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
    //��ġ�Ϸ������
    public void NoDrag()
    {
        // woodmoney ���̰�, ��ȭ���� ������ �Ѱ������
        main.OnClickBuildingBack();
        GameManager.Instance.goodsManager.wood -= buildingWoodMoney;
        GameManager.Instance.goodsManager.enhance += enhance;
        Debug.Log("All Enhance: " + GameManager.Instance.goodsManager.enhance);

        //AllModels[selectionIndex].gameObject.GetComponent<Renderer>().material.color = new Color(233, 79, 55);
        var bild = Instantiate(AllModels[selectionIndex]);// AllModels[selectionIndex].transform.position = currentScreenSpace;
        bild.transform.position = currentScreenSpace;
        buildList.Add(bild);
        AllModels[selectionIndex].SetActive(false);
        if (bild.tag == "Finish")
        {
            // prevCook
            buildList.Remove(prevCook);
            Destroy(prevCook);
        }


        IsMove = false;
        Save();
        selectionIndex = beforeSelect;
    }

    //��ġ���(���ư����ư ��������)
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
        //���� ������ ����Ʈ�� Ŭ���� Ÿ�Ը� �߰�
        curType = tapName;
        CurItemList = AllItemList.FindAll(x => x.Type == tapName);

        //������ �̹����� ��������� �ߵ���
        for (int i = 0; i < Slot.Length; i++)
        {
            //���԰� �ؽ�Ʈ ���̱�
            bool isExist = i < CurItemList.Count;
            Slot[i].SetActive(isExist);

            if (isExist)
            {
                ItemImage[i].sprite = ItemSprite[AllItemList.FindIndex(x => x.Index == CurItemList[i].Index)];
                UsingImage[i].SetActive(CurItemList[i].isUsing);
            }
        }

        //int tabNum = 0;
        //switch (tapName)
        //{
        //    case "Building": tabNum = 0; break;
        //    case "FrontFurniture": tabNum = 1; break;
        //    case "BackFurniture": tabNum = 2; break;

        //}


        // �� �̹��� ��ȯ
        //for (int i = 0; i < slotbu.Length; i++)
        //{
        //    slotbu[i].interactable = i == tabNum ? true : false;
        //}


    }



    //������ �޴��� ������ �ߵ��� �ε�
    public void PointerClick(int slotNum)
    {
        buildingWoodMoney = int.Parse(CurItemList[slotNum].WoodMoney);

        if (buildingWoodMoney > GameManager.Instance.goodsManager.wood)
        {
            checkbtn.interactable = false;
        }
        else
        {
            checkbtn.interactable = true;
        }

        //checkbtn.GetComponent<TextMeshProUGUI>().text = 

        wood.SetActive(true);

        ExplainPanel.GetComponentInChildren<TextMeshProUGUI>().text = CurItemList[slotNum].Explain;
        ExplainPanel.transform.GetChild(2).GetComponentInChildren<Image>().sprite = Slot[slotNum].transform.GetChild(0).GetComponent<Image>().sprite;
        ExplainPanel.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "��� ���� ü�� �� ���ݷ�" + CurItemList[slotNum].SpecOfEnhance + "% ����";
        ExplainPanel.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "(���� ��" + CurItemList[slotNum].SpecOfLevel + "% ����)";
        ExplainPanel.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = CurItemList[slotNum].SpecOfContent;
        wood.GetComponentInChildren<TextMeshProUGUI>().text = CurItemList[slotNum].WoodMoney;

    }

    void Save()
    {

        string jdata = JsonUtility.ToJson(new Serialization<Item>(AllItemList));
        File.WriteAllText(filepath, jdata); //text ����


        buildingInfoList.Clear();
   
        foreach (var i in buildList)
        {
            //var info = i.GetComponent<ModelsInfo>();
            //buildingInfoList.Add(new BuildingInfo(info.modelIndex, i.transform.position, info.curModelNum, info.buyModelNum));
            buildingInfoList.Add(new BuildingInfo(selectionIndex, i.transform.position));
        }

        string bdata = JsonUtility.ToJson(new Serialization<BuildingInfo>(buildingInfoList));
        File.WriteAllText(filepath2, bdata); //text ����

        TapClick(curType);
    }

    void Load()
    {
        if (!File.Exists(filepath))
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
            //�����Ͱ� ������ 1�� ������Ʈ�� true�ǵ���
            buildingInfoList.Clear();
            for (int i = 0; i < AllModels.Length; i++)
            {
                //   buildingInfoList.Add(new BuildingInfo(i, 0f));
            }
            // buildingInfoList[0].Istrue = true;
        }
        TapClick(curType);

        for (var i = 0; i < buildingInfoList.Count; ++i)
        {
            var models = Instantiate(AllModels[buildingInfoList[i].activIndex]);
            models.transform.position = buildingInfoList[i].position;
            models.SetActive(true);
            buildList.Add(models);
        }

    }
}