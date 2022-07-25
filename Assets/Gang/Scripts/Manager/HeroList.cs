using UnityEngine;

[CreateAssetMenu(fileName = "HeroList", menuName = "ScriptableObject/HeroList")]
public class HeroList : ScriptableObject
{
    public GameObject[] heroPrefab;
    public bool[] isSellect;
}
