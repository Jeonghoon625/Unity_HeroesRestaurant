using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class s1wildBoar : MonoBehaviour
{
    public GameObject boarPrefab;
    private int count = 3;
    public float monsterHp = 100;
    public float monsterDamage = 1;
    public float spawnTime;
    private TextMeshProUGUI damage;

    private void Awake()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        var pos = transform.position;
        for (int i = 0; i < count; i++)
        {
            pos.x += 0.5f * i;
            transform.position = pos;
            Instantiate(boarPrefab, transform.position, Quaternion.identity);
        }
        yield return new WaitForSeconds(spawnTime);
    }
}
