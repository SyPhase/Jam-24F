using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    [SerializeField] float speed = 1f;
    Rigidbody rigidbody;

    // Awake is like Start(), but called very first
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        rigidbody.velocity = speed * transform.up;
    }

    void OnCollisionEnter(Collision collision)
    {
        // EXPLODE + DAMAGE (Spawn Explosion?)
        explosion.SetActive(true);

        // Deactivate this projectile
        //gameObject.SetActive(false);
        StartCoroutine(SelfDestruct()); // Starts a coroutine to destroy object after a few seconds
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject); // Permanently delete this projectile instance
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IDamageable hit))
        {
            hit.Damage();
        }
    }

    void OnDisable()
    {
        rigidbody.velocity = Vector3.zero;
    }
}