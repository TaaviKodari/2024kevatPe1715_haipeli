using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnInterval = 2f;

    private float nextSpawnTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time < nextSpawnTime){
            return;
        }

        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        
        GameObject enemy = EnemyPoolManager.Instance.GetEnemy();
        if(enemy != null){
            enemy.transform.position = transform.position;
        }

        nextSpawnTime = Time.time + spawnInterval; 
    }
}
