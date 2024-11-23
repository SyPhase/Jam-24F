using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
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

        // Deactivate this projectile
        gameObject.SetActive(false);
    }

    void OnDisable()
    {
        rigidbody.velocity = Vector3.zero;
    }
}