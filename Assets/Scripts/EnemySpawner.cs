using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float _widthRange;
    [SerializeField] private float _heightRange;
    [SerializeField] private GameObject _enemy;
    [SerializeField] private int enemyGoal;

    void Start()
    {
        for (int i = 0; i < enemyGoal; i++)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Vector2 spawnPos = new(Random.Range(-_widthRange, _widthRange), Random.Range(-_heightRange, _heightRange));
        Instantiate(_enemy, spawnPos, Quaternion.identity);
    }
}
