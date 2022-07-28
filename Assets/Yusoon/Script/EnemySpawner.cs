using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Wave[] waves;
    //public Enemy enemy;

    private Wave currentWave;
    private int currentWaveNumber = -1;      //���� �� ��° ���̺�����.

    private int monsterRemains;          //��ȯ �Ǿ���ϴ� ����
    private int enemyRemainingAlive;    //�����ִ� ����

    private float startWaitTime = 4f;
    private float waveWaitTime = 1.5f;

    private StageManager stageManager;
    private void Start()
    {
        StartCoroutine(NextWave(startWaitTime));

        stageManager = GameObject.FindWithTag("GameController").GetComponent<StageManager>();
    }
    private void OnEnemyDeath()
    {
        enemyRemainingAlive--;

        if (enemyRemainingAlive == 0)
        {
            StartCoroutine(NextWave(waveWaitTime));
        }
    }
    IEnumerator NextWave(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        currentWaveNumber++;

        if (currentWaveNumber < waves.Length)
        {
            currentWave = waves[currentWaveNumber];

            monsterRemains = currentWave.enemies.Count;
            enemyRemainingAlive = monsterRemains;

            for (int i = 0; i < monsterRemains; i++)
            {
                var pos = currentWave.pos[i];
                Quaternion rot = currentWave.enemies[i].transform.rotation;
                if (pos.x > 0)
                {
                    rot.y += 180f;
                }
                else
                {
                    rot.y += 0f;
                }

                Enemy spawnedEnemy = Instantiate(currentWave.enemies[i], pos, rot);
                spawnedEnemy.OnDeath += OnEnemyDeath;
                var ren = spawnedEnemy.GetComponent<Renderer>();
                ren.enabled = false;
                yield return 0;
                ren.enabled = true;
                //Debug.LogError("!");
                //yield return 0;
            }
        }
        else
        {
            stageManager.Victory();
        }
        yield break;
    }

    [System.Serializable]
    public class Wave
    {
        public List<Enemy> enemies;// = new List<Enemy>();
        public List<Vector3> pos;// = new List<Vector3>();
    }
}
