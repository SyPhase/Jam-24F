using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    // Can be set in the Inspector
    [SerializeField] float xAxisSensitivity = 10f;
    [SerializeField] float yAxisSensitivity = 10f;

    [SerializeField] float xAxisMaxSpeed = 1f;

    // Private variables
    Rigidbody rigidbody;

    float xAxis = 0f;
    float yAxis = 0f;
    float fire = 0f;

    // Start is called once when the scene finishes loading
    private void Start()
    {
        // Get reference to rigidbody component on this object
        rigidbody = GetComponent<Rigidbody>();

        // Set maximum angular velocity (clamp) so it won't spin too fast
        rigidbody.maxAngularVelocity = xAxisMaxSpeed;
    }

    // FixedUpdate is called once per physics time set (0.02s)
    void FixedUpdate()
    {
        // Get and store Inputs
        xAxis = Input.GetAxis("Horizontal");
        yAxis = Input.GetAxis("Vertical");
        fire = Input.GetAxis("Jump");

        // If no xAxis input
        if (xAxis == 0f)
        {
            // Set angular velocity to zero if very low
            if (0.1f > rigidbody.angularVelocity.y && -0.1f < rigidbody.angularVelocity.y)
            {
                rigidbody.angularVelocity = Vector3.zero;
            }
            // if rotating right, set input to counter-rotate left
            else if (0 < rigidbody.angularVelocity.y)
            {
                //xAxis = -1f;
                xAxis = -1f + -rigidbody.angularVelocity.y;
            }
            // if rotating left, set input to counter-rotate right
            else
            {
                //xAxis = 1f;
                xAxis = 1f + -rigidbody.angularVelocity.y;
            }
        }
        // if input turn right, but turret still moving left
        else if (0 < xAxis && 0 > rigidbody.angularVelocity.y)
        {
            xAxis = 1f + -rigidbody.angularVelocity.y;
        }
        // if input turn left, but turret still moving right
        else if (0 > xAxis && 0 < rigidbody.angularVelocity.y)
        {
            xAxis = -1f + -rigidbody.angularVelocity.y;
        }

        // Rotate turret arount y-axis by adding torque (ForceMode isn't important)
        rigidbody.AddTorque(0f, xAxis * xAxisSensitivity, 0f, ForceMode.Acceleration);

        // Debug
        print("y Angular vel = " + rigidbody.angularVelocity.y);
        print("xAxis = " + xAxis);
    }
}
