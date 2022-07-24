using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName ="Card/Card")]
public class CardData : ScriptableObject
{
    [SerializeField]
    private string cardName;
    public string CardName { get { return cardName; } }

    [SerializeField]
    public GameObject[] cardPrefab;
}
