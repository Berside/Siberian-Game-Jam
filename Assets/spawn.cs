using UnityEngine;
public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 60f;
    private float timer;
    private float count;

    private void Start()
    {
        timer = spawnInterval;
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = spawnInterval - count;
            SpawnEnemy();
        }
    }
    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        count = count + 5;
    }
}

