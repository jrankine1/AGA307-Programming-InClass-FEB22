using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : GameBehaviour
{
    public EnemyType myType;
    int baseHealth = 100;
    public int myHealth;
    public float mySpeed;
    float baseSpeed = 1;

    [Header("AI")]
    public PatrolType myPatrol;
    int patrolPoint = 0;
    bool reverse = false;
    Transform startPos;
    Transform endPos;
    public Transform moveToPos;
    
    
    void Start()
    {
        SetupEnemy();
        SetupAI();
        StartCoroutine(Move());
    }

    void SetupAI()
    {
        
        startPos = transform;
        endPos = _EM.GetRandomSpawnPoint();
        moveToPos = endPos;
    }

    void SetupEnemy()
    {
        switch(myType)
        {
            case EnemyType.Archer:
                myHealth = baseHealth / 2;
                mySpeed = baseSpeed * 2;
                myPatrol = PatrolType.Random;
                break;
            case EnemyType.OneHand:
                myHealth = baseHealth;
                mySpeed = baseSpeed;
                myPatrol = PatrolType.linear;
                break;
            case EnemyType.TwoHand:
                myHealth = baseHealth * 2;
                mySpeed = baseSpeed / 2;
                myPatrol = PatrolType.Loop;
                break;
            default:
                myHealth = baseHealth;
                mySpeed = baseSpeed;
                myPatrol = PatrolType.Random;
                break;
        }
    }
   
    IEnumerator Move()
    {

        switch(myPatrol)
        {
            case PatrolType.Random:
                moveToPos = _EM.GetRandomSpawnPoint();
                break;
            case PatrolType.linear:
                moveToPos = _EM.spawnPoints[patrolPoint];
                patrolPoint = patrolPoint != _EM.spawnPoints.Length ? patrolPoint + 1 : 0;
                break;
            case PatrolType.Loop:
                moveToPos = reverse ? startPos : endPos;
                reverse = !reverse;
                break;
        }

        while (Vector3.Distance(transform.position, moveToPos.position) > 0.3f)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveToPos.position, Time.deltaTime * mySpeed);
            transform.rotation = Quaternion.LookRotation(moveToPos.position);
            yield return null;
        }
        yield return new WaitForSeconds(1);
        StartCoroutine(Move());
    }

    void Hit( int _damage)
    {
       
        myHealth -= _damage;
        if (myHealth <= 0)
        {
            GameEvents.ReportEnemyDied(this);
        }
        else
        {
            GameEvents.ReportEnemyHit(this);
        }
            
    }

    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
            Hit(20);
    }
}
