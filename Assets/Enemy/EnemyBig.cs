using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBig : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    Enemy thisEnemy;

    void Start()
    {
        thisEnemy = GetComponent<Enemy>();
    }

    void OnDestroy()
    {
        for (int i = 0; i < thisEnemy.GetHealth(); i++)
        {
            Instantiate(enemyPrefab, transform.position, transform.rotation);
        }
    }
}
