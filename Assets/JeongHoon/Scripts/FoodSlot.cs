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

    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
        image.sprite = sprite;

        Debug.Log(id);
        Debug.Log(title);
        Debug.Log(explanation);
    }

    public void OnClick()
    {
        GameObject infoPanel = GameObject.FindGameObjectWithTag("FoodInfoPanel");
        if (infoPanel != null)
        {
            infoPanel.GetComponent<InformationLoadManager>().ShowInfo();
        }
    }
}
