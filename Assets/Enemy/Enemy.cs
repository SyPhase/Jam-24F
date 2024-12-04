using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void Damage();
}

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] int health = 1;
    EnemyFollow enemyFollow;

    void Start()
    {
        enemyFollow = GetComponent<EnemyFollow>();
    }

    public void Damage()
    {
        health--; // minus one health

        if (health <= 0)
        {
            StartCoroutine(SelfDestruct()); // Starts a coroutine to destroy object after a few seconds
        }
    }

    IEnumerator SelfDestruct()
    {
        enemyFollow.Kill();

        yield return new WaitForSeconds(3f);

        Destroy(gameObject); // Permanently delete this enemy instance
    }
}
