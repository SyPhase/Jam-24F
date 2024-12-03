using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField] float speed = 0.02f;
    [Tooltip("If set to zero, floats toward")]
    [SerializeField] float gravity = -0.1f;
    Transform target;
    CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        Vector3 motion = -(transform.position - Vector3.MoveTowards(transform.position, target.position, speed));
        motion.y = gravity;

        characterController.Move(motion);
    }
}
