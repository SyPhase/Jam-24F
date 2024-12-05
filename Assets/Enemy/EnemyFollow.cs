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
    bool isDead = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        target = FindObjectOfType<Tower>().transform; // Finds the base of the tower as target
    }

    void FixedUpdate()
    {
        Vector3 motion = Vector3.zero;

        if (!isDead)
        {
            motion = -(transform.position - Vector3.MoveTowards(transform.position, target.position, speed));
            motion.y = gravity;
        }
        else
        {
            motion.y = 25f * -gravity;
        }

        characterController.Move(motion);
    }

    public void Kill()
    {
        isDead = true;
    }
}
