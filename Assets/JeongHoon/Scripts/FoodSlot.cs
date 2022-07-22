using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodSlot : MonoBehaviour
{
    public int id;
    public string title;
    public string explanation;
    public Sprite sprite;
    public Image image;

    public int maxReserve;

    public List<int> currencyList;

    public int stage;
    public GameObject lockGO;

    public Color color;
    public Button button;

    private void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
    }

    private void Start()
    {
        image.sprite = sprite;
    }

    public void Lock()
    { 
        lockGO.SetActive(true);
        image.color = Color.black; 
        button.enabled = false;
    }

    public void Open()
    {
        lockGO.SetActive(false);
        image.color = Color.white;
        button.enabled = true;
    }
    /*
    public void OnClick()
    {
        GameObject infoPanel = GameObject.FindGameObjectWithTag("FoodInfoPanel");
        if (infoPanel != null)
        {
            infoPanel.GetComponent<InformationLoadManager>().ShowInfo();
        }
    }
    */

}
