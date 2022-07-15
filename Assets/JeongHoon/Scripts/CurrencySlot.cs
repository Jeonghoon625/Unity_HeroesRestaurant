using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencySlot : MonoBehaviour
{
    public int id;
    public string title;
    public Sprite sprite;

    public GameObject imageGO;
    private Image image;

    private void Start()
    {
        image = imageGO.GetComponent<Image>();
        image.sprite = sprite;
    }
}
