using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class s1wildWolf : MonoBehaviour
{
    public GameObject wolfPrefab;
    public float monsterHp = 150;
    public float monsterDamage = 3;
    public float spawnTime;
    private TextMeshProUGUI damage;

    private void Awake()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        Instantiate(wolfPrefab, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(spawnTime);
    }
}
