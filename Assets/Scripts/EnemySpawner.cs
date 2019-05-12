using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float TimeBetweenSpawns = 10f;
    public int MaxSpawnedEnemies = 2;
    public GameObject Spawneable;
    public Transform SpawnPoint;

    private float currentTimer;


    private void Awake()
    {
        currentTimer = TimeBetweenSpawns;
    }

    private void Update()
    {
        currentTimer -= Time.fixedDeltaTime;
        if (currentTimer <= 0 && SpawnPoint.childCount < MaxSpawnedEnemies)
        {
            doSpawn();
            currentTimer = TimeBetweenSpawns;
        }
    }

    private void doSpawn()
    {
        Instantiate(Spawneable, SpawnPoint);
    }
}
