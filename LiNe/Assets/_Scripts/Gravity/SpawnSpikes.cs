﻿using UnityEngine;

public class SpawnSpikes : MonoBehaviour
{
    [SerializeField] private ObstacleData[] obstaclesData;

    private ObjectPool objectPool;

    private PlayerBehaviour playerBehaviour;

    private int startIndex = 0, lastIndex = 1;
    private int spawnOffset = 7;
    private int targetScoreToSpawn = 5;

    private GameObject lastObstacle;


    private void Start()
    {
        objectPool = new ObjectPool(obstaclesData);

        playerBehaviour = FindObjectOfType<PlayerBehaviour>();

        Spawn();
    }

    private void Update()
    {
        if (Vector3.Distance(playerBehaviour.transform.position, lastObstacle.transform.position) < 5)
            Spawn();

    }

    private void Spawn()
    {
        if (playerBehaviour.PlayerScore >= targetScoreToSpawn)
        {
            targetScoreToSpawn += 5;

            if (lastIndex < obstaclesData.Length - 1)
            {
                if (lastIndex > 5) startIndex++;
                lastIndex++;
            }
        }


        int randomObstacle = Helpers.GenerateRandomNumber(startIndex, lastIndex);

        Vector2 spawnPosition = Vector2.right * spawnOffset;

        lastObstacle = objectPool.GetFromPool(obstaclesData[randomObstacle].ObstacleName, spawnPosition, Quaternion.identity);

        spawnOffset += (int)obstaclesData[randomObstacle].NextSpawnPoint;
    }
}
