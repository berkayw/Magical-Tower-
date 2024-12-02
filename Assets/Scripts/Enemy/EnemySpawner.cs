using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    [SerializeField] private GameObject[] enemyPrefabs; 
    [SerializeField] private Transform spawnPoint; 
    [SerializeField] private float spawnRate = 2f; 
    private float spawnCooldown;

    [Header("Game Difficulty Settings")]
    [SerializeField] private float difficultyIncreaseRate = 0.1f; 
    
    void Start()
    {
        spawnCooldown = spawnRate; 
    }

    void Update()
    {
        spawnCooldown -= Time.deltaTime;

        if (spawnCooldown <= 0f)
        {
            SpawnEnemy();
            spawnCooldown = spawnRate; 
            spawnRate = Mathf.Max(0.5f, spawnRate - difficultyIncreaseRate);
        }
    }

    void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        
        float randomSign;
        
        randomSign = Random.Range(0f, 1f) < 0.5f ? -1f : 1f;
        float spawnPointX = randomSign * Random.Range(7, 9);
        randomSign= Random.Range(0f, 1f) < 0.5f ? -1f : 1f;
        float spawnPointZ = randomSign * Random.Range(7, 9);
        
        spawnPoint.position = new Vector3(spawnPointX, enemyPrefabs[randomIndex].transform.localScale.y / 2, spawnPointZ);
        GameObject enemy = Instantiate(enemyPrefabs[randomIndex], spawnPoint.position, Quaternion.identity);
        
    }
}