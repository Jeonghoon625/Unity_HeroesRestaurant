using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecipeCurrencySlot : MonoBehaviour
{
    public GameObject imageGO;
    public Image image;
    public TextMeshProUGUI currentQuantity;
    public int initialQuantity;

    public void Init(Sprite sprite, string text)
    {
        image = imageGO.GetComponent<Image>();
        image.sprite = sprite;
        initialQuantity = int.Parse(text);
        currentQuantity.text = text;
    }
}
