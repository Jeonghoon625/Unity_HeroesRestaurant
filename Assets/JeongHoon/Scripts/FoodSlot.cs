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

    public List<int> currencyList;

    private void Start()
    {
        image = GetComponent<Image>();
        image.sprite = sprite;
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
