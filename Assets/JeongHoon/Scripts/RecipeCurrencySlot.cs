using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecipeCurrencySlot : MonoBehaviour
{
    public GameObject imageGO;
    public Image image;
    public TextMeshProUGUI quantityText;

    public void Change(Sprite sprite, string text)
    {
        image = imageGO.GetComponent<Image>();
        image.sprite = sprite;
        quantityText.text = text;
    }
}
