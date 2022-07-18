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
    private int enemyRemainingAlive;

    private void Start()
    {
        NextWave();
    }

    private void OnEnemyDeath()
    {
        enemyRemainingAlive--;

        if(enemyRemainingAlive == 0)
        {
            NextWave();
        }
    }

    private void NextWave()
    {
        currentWaveNumber++;
       
        if (currentWaveNumber - 1 < waves.Length)
        {
            currentWave = waves[currentWaveNumber - 1];

            monsterRemains = currentWave.enemys.Count;
            enemyRemainingAlive = monsterRemains;

            for(int i = 0; i < monsterRemains; i++)
            {
                var pos = currentWave.pos[i];
                Quaternion rot = Quaternion.identity;
                if(pos.x > 0)
                {
                    rot.y = 180f;
                }
                else
                {
                    rot.y = 0f;
                }

                Enemy spawnedEnemy = Instantiate(currentWave.enemys[i], pos, rot);
                spawnedEnemy.OnDeath += OnEnemyDeath;
            }
        }
    }

    [System.Serializable]
   public class Wave
    {
        public List<Enemy> enemys = new List<Enemy>();
        public List<Vector3> pos = new List<Vector3>();
    }
}
