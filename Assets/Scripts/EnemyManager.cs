using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType { OneHand, TwoHand, Archer};
public enum PatrolType { linear, Random, Loop};

public class EnemyManager : GameBehaviour<EnemyManager>
{
    public string[] enemyNames;
    public Transform[] spawnPoints;
    public GameObject[] enemyTypes;

    public List<GameObject> enemies;
    
    void Start()
    {
        //StartCoroutine(SpawnEnemyDelay());
        //for (int i = 0; i < 101; i++)
        //{
        //    print(i);
        //}
    }

    private void Update()
    {
        
        //if (Input.GetKeyDown(KeyCode.K))
        //    KillAllEnemies();

        //if (Input.GetKeyDown(KeyCode.B))
        //    KillSpecificEnemy("_B");

        
    }
    
    /// <summary>
    /// Spawns Enemies at spawn point locations
    /// </summary>
    void SpawnEnemy()
    {
        
        for(int i = 0; i < spawnPoints.Length; i++)
        {
            int rnd = Random.Range(0, enemyTypes.Length);
            GameObject og = Instantiate(enemyTypes[rnd], spawnPoints[i].position, spawnPoints[i].rotation);
            enemies.Add(og);
        }
    }
    IEnumerator SpawnEnemyDelay()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            int rnd = Random.Range(0, enemyTypes.Length);
            GameObject og = Instantiate(enemyTypes[rnd], spawnPoints[i].position, spawnPoints[i].rotation);
            enemies.Add(og);
            yield return new WaitForSeconds(2);
        }

    }
    public Transform GetRandomSpawnPoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Length)];
    }

    void KillSpecificEnemy(string _condition)
    {
        int eCount = enemies.Count;
        for (int i = 0; i < eCount; i++)
        {
            if (enemies[0].name.Contains(_condition))
                KillEnemy(enemies[0]);
        }
    }

    /// <summary>
    /// Kills all enemies in the scene
    /// </summary>
    void KillAllEnemies()
    {
        int eCount = enemies.Count;
        for (int i = 0; i < eCount; i++)
        {
            KillEnemy(enemies[0]);
        }
    }

    /// <summary>
    /// Kills an enemy based on the gameobject passed in
    /// </summary>
    /// <param name="_enemy">The GameObject of the Enemy</param>
    public void KillEnemy(GameObject _enemy)
    {
        if (enemies.Count == 0)
            return;

        Destroy(_enemy);
        enemies.Remove(_enemy);
    }

    void OnEnemyDied(Enemy _enemy)
    {
        KillEnemy(_enemy.gameObject);
    }

    void OnGameStateChange(GameState _gameState)
    {
        switch (_gameState)
        {
            case GameState.Playing:
                StartCoroutine(SpawnEnemyDelay());
                break;
            case GameState.Title:
            case GameState.GameOver:
            case GameState.Paused:
                StopAllCoroutines();
                break;
        }
    }

    private void OnEnable()
    {
        
        GameEvents.OnEnemyDied += OnEnemyDied;
        GameEvents.OnGameStateChange += OnGameStateChange;
    }

    private void OnDisable()
    {
        
        GameEvents.OnEnemyDied -= OnEnemyDied;
        GameEvents.OnGameStateChange -= OnGameStateChange;
    }
}
