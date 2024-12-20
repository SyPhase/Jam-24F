using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    // Can be set in the Inspector
    [SerializeField] float xAxisSensitivity = 10f;
    [SerializeField] float yAxisSensitivity = 0.2f;

    [SerializeField] float xAxisMaxSpeed = 1f;
    [SerializeField] float fireDelay = 1f;

    [SerializeField] Transform model;
    [SerializeField] Transform leftBarrel;
    [SerializeField] Transform rightBarrel;
    [SerializeField] GameObject projectile;

    [SerializeField] AudioSource horizontalTurnSound;
    [SerializeField] AudioSource verticalTurnSound;

    // Private variables
    Rigidbody rigidbody;

    float xAxis = 0f;
    float yAxis = 0f;
    float fire = 0f;
    float timeSinceFire = 0f;
    float lastFireState = 0f;
    bool fireLeft = true;

    // Start is called once when the scene finishes loading
    private void Start()
    {
        // Get reference to rigidbody component on this object
        rigidbody = GetComponent<Rigidbody>();

        // Set maximum angular velocity (clamp) so it won't spin too fast
        rigidbody.maxAngularVelocity = xAxisMaxSpeed;

        // Setup Rotatinf SFX
        horizontalTurnSound.Play();
        horizontalTurnSound.Pause();
        verticalTurnSound.Play();
        verticalTurnSound.Pause();
    }

    // FixedUpdate is called once per physics time set (0.02s)
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { Application.Quit(); } // Exit game is "Esc" is pressed

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

        // SFX
        if (!horizontalTurnSound.isPlaying)
        {
            if (xAxis != 0f)
            {
                horizontalTurnSound.UnPause(); // Start turning SFX
            }
        }
        else if (xAxis == 0f)
        {
            horizontalTurnSound.Pause(); // Stop turning SFX
        }

        // Debug
        //print("y Angular vel = " + rigidbody.angularVelocity.y);
        //print("xAxis = " + xAxis);

        if (yAxis != 0)
        {
            Vector3 eAngle = model.transform.localRotation.eulerAngles;
            eAngle.z += -yAxis * yAxisSensitivity;

            //if (eAngle.z >) // 60 low (65) : 30 high
            if (eAngle.z <= 30f)
            {
                eAngle.z = 30f;
            }
            else if (eAngle.z >= 65f)
            {
                eAngle.z = 65f;
            }

            Quaternion qAngle = Quaternion.Euler(eAngle);
            model.transform.localRotation = qAngle;

            bool atMaxAngle = false;
            if (eAngle.z == 65f || eAngle.z == 30f)
            {
                atMaxAngle = true;
            }

            // SFX
            if (!verticalTurnSound.isPlaying)
            {
                if (!atMaxAngle)
                {
                    verticalTurnSound.UnPause(); // Stop turning SFX
                }
            }
            else if (atMaxAngle)
            {
                verticalTurnSound.Pause();
            }
        }
        else
        {
            verticalTurnSound.Pause(); // Stop turning SFX
        }

        timeSinceFire += Time.fixedDeltaTime;
        if (fire != lastFireState && fire != 0 && timeSinceFire > fireDelay)
        {
            timeSinceFire = 0f;

            Vector3 firePos;
            if (fireLeft)
            {
                firePos = leftBarrel.position;
                fireLeft = false;
            }
            else
            {
                firePos = rightBarrel.position;
                fireLeft = true;
            }

            Instantiate(projectile, firePos, leftBarrel.rotation);
        }
        lastFireState = fire;
    }
}
