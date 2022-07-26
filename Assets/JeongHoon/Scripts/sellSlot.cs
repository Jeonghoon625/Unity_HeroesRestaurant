using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class sellSlot : MonoBehaviour
{
    public Sprite sprite;
    public Image image;
    public int sum;

    public TextMeshProUGUI sumText;

    private void Start()
    {
        image.sprite = sprite;
        sumText.text = sum.ToString();
    }
}
