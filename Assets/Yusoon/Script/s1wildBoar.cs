using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s1wildBoar : MonoBehaviour
{
    public GameObject boarPrefab;
    public int count = 3;
    public float monsterHp = 100;
    public float monsterDamage = 1;
    public float spawnTime;

    private Transform target;
    private float speed = 2f;

    private void Awake()
    {
        StartCoroutine(Spawn());
    }
    IEnumerator Spawn()
    {
        for(int i = 0; i < count; i++)
        {
            Vector3 pos = transform.position;
            pos.x = 0.5f * i;
            transform.position = pos;
            Instantiate(boarPrefab, pos, Quaternion.identity);
        }
        yield return new WaitForSeconds(spawnTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            target = other.gameObject.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        target = null;
    }
}
