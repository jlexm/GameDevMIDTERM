using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclePrefabs;
    public float obstacleSpawnTime = 5f;
    public float obstacleSpeed = 1f;

    private GameObject player;

    private float timeUntilObstacleSpawn;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        timeUntilObstacleSpawn = 0f;
    }

    private void Update()
    {
        SpawnLoop();
    }

    private void SpawnLoop()
    {
        timeUntilObstacleSpawn += Time.deltaTime;
        if (timeUntilObstacleSpawn >= obstacleSpawnTime)
        {
            Spawn();
            timeUntilObstacleSpawn = 0f;
        }
    }

    private void Spawn()
    {
        GameObject obstacleToSpawn = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
        GameObject spawnedObstacle = Instantiate(obstacleToSpawn, transform.position, Quaternion.identity);
        Vector3 directionToPlayer = (player.transform.position - spawnedObstacle.transform.position).normalized;

 
        Rigidbody2D obstacleRB = spawnedObstacle.GetComponent<Rigidbody2D>();
        obstacleRB.velocity = directionToPlayer * obstacleSpeed;
    }

    public void UpdateObstacleSpawnTime(float playerDistance)
    {
 
        int distance500Count = Mathf.FloorToInt(playerDistance / 200);
        obstacleSpawnTime = Mathf.Max(0.5f, 5f - distance500Count);
    }
}