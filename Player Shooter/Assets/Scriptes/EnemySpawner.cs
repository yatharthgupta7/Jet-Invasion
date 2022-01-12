using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemy1;
    [SerializeField] GameObject[] boss;
    [SerializeField] Transform[] enemySpawnPosition;
    [SerializeField] Transform[] bossSpawnPosition;

    public bool enemySpawn;
    public bool bossSpawn;

    public int countEnemy;
    public int countBoss;
    void Start()
    {
    }

    void Update()
    {
        countEnemy = GameObject.FindGameObjectsWithTag("Enemy").Length;
        countBoss = GameObject.FindGameObjectsWithTag("Boss").Length;
        if(countEnemy>=3)
        {
            enemySpawn = false;
        }
    }

   private void FixedUpdate()
    {
        if(enemySpawn && !bossSpawn && countEnemy<3)
        {
            SpawnEnemy(3-countEnemy);
        }

        /*if(bossSpawn&&!enemySpawn && countBoss==0)
        {
            SpawnBoss();
        }*/
    }

    public void EnemySpawnUpdate()
    {
        enemySpawn = true;
    }
    public void SpawnOneEnemy()
    {
        Instantiate(enemy1, enemySpawnPosition[Random.Range(0,enemySpawnPosition.Length)].position, Quaternion.identity);
    }

    public void SpawnEnemy(int count)
    {

        for(int x=0;x<count;x++)
        {
            Instantiate(enemy1, enemySpawnPosition[x].position, Quaternion.identity);
        }

    }

    public void SpawnBoss()
    {
        if(countEnemy!=0)
        {
            return;
        }
        for(int x=0;x<boss.Length;x++)
        {
            boss[x].GetComponent<BossLogic>().toPoint = bossSpawnPosition[x].transform;
            Instantiate(boss[x], transform.position, Quaternion.identity);
        }
    }
}
