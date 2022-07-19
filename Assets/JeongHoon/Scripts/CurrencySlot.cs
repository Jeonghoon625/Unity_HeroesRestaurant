using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CurrencySlot : MonoBehaviour
{
    public int id;
    public string title;
    public Sprite sprite;

    public GameObject imageGO;
    public Image image;

    public TextMeshProUGUI reserveText;

    private void Start()
    {
        image = imageGO.GetComponent<Image>();
        image.sprite = sprite;
    }
}
