using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] obstcalePrefab;
    public float respawnTime = 30.0f;

    [SerializeField] float spawnChance;

    private float secondsLeftTillSpawn = 0;
    private Vector2 screenBounds;
    float obstacleSpeed=10f;
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        //StartCoroutine(ObstacleWave());
    }
    private void SpawnEnemy(float speed)
    {
        GameObject a = Instantiate(obstcalePrefab[Random.Range(0,obstcalePrefab.Length) ])as GameObject;
        a.GetComponent<ObstacleLogic>().moveSpeed = obstacleSpeed;
        a.transform.position = new Vector2(20f, Random.Range(transform.position.y, transform.position.y-9.2f));
    }


    IEnumerator ObstacleWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            SpawnEnemy(obstacleSpeed);
        }
    }

    private void Update()
    {
        secondsLeftTillSpawn -= Time.deltaTime;
        int temp = Random.Range(0, 100);
        if(temp>=spawnChance&&secondsLeftTillSpawn<=0)
        {
            SpawnEnemy(obstacleSpeed);
            secondsLeftTillSpawn = respawnTime;
        }
    }

    public void LevelUp()
    {
        obstacleSpeed = GameManager.Instance.SetSpeedObstacle();
    }
}
