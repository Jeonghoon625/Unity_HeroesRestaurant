using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class s1wildBoar : MonoBehaviour
{
    public GameObject[] boarPrefab;
    private MonsterController monsterController;
    public float monsterHp = 100;
    public float monsterDamage = 1;
    private TextMeshProUGUI damage;
    private BoxCollider boarCollider;

    private void Awake()
    {
        boarCollider = GetComponent<BoxCollider>();
        for(int i = 0; i < boarPrefab.Length; i++)
        {
            GameObject boar = Instantiate(boarPrefab[0], transform.position, Quaternion.identity);
        }


    }
}
