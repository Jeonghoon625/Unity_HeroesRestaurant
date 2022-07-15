using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Wave[] waves;
    public Enemy enemy;

    private Wave currentWave;
    private int currentWaveNumber;

    private int monsterRemains;
    private float nextSpawnTime;

    private void Start()
    {
        NextWave();
    }
    private void Update()
    {
        if(monsterRemains > 0 &&Time.time > nextSpawnTime)
        {
            monsterRemains--;
            nextSpawnTime = Time.time + currentWave.timeBetweenSpawns;

            Enemy spawnedEnemy = Instantiate(enemy, Vector3.zero, Quaternion.identity) as Enemy;
        }
    }
    private void NextWave()
    {
        currentWaveNumber++;
        currentWave = waves[currentWaveNumber - 1];

        monsterRemains = currentWave.monsterCount;
    }

    [System.Serializable]
   public class Wave
    {
        public int monsterCount;
        public float timeBetweenSpawns;
    }
}
