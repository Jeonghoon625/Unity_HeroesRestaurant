using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System;

public class s1wildBoarSpawn : MonoBehaviour
{
    public GameObject boarPrefab;
    public int count = 3;
    public float monsterDamage = 1;
    public float spawnTime;
    public float spawnTimer = 0f;

    private Transform target = null;
    private float moveSpeed = 2f;

    public Slider sliderHp;
    public int maxHp = 100;
    private int currentHp;
    public int hp
    {
        get { return currentHp; }
        set 
        { 
            currentHp = value;
            sliderHp.value = currentHp;
        }
    }

    public Animator anim;

    private void Start()
    {
        currentHp = maxHp;
        sliderHp.maxValue = maxHp;
        sliderHp.value = currentHp;
    }
    private void Awake()
    {
        spawnTimer = 0f;
        Vector3 pos = transform.position;
        for (int i = 0; i < count; i++)
        {
            pos.x = 0.5f * i;
            Instantiate(boarPrefab, pos, Quaternion.identity);
        }
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnTime)
        {
            
        }
    }
}
