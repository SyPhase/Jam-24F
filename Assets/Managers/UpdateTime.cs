using UnityEngine;
using TMPro;

public class UpdateTime : MonoBehaviour
{
    [Tooltip("(Canvas) UI timer text")]
    [SerializeField] TMP_Text timeText;

    [SerializeField] Light directionalLight;
    [SerializeField] Transform sunParent;
    [Tooltip("Speed at which the 'sun' will rotate. Bigger numbers is faster")]
    [SerializeField] float sunRotationSpeed = 0.01f;

    float gameTime = 0f;

    void Start()
    {
        // Initializes the directional lights position if moved
        directionalLight.transform.LookAt(sunParent);
    }

    void FixedUpdate()
    {
        // Keeps time
        gameTime += Time.fixedDeltaTime;

        UpdateUI();
        RotateDirectionalLight();
    }

    // Updates the timer text
    void UpdateUI()
    {
        string temp = gameTime.ToString();
        temp = temp.Substring(0, temp.IndexOf(".")); // sometimes "." or "," because localization bug...

        // TODO : Format timer like 00:00

        timeText.text = temp;
    }

    // Rotated the directional light's parent
    void RotateDirectionalLight()
    {
        // Rotates the sun
        sunParent.Rotate(0f, 0f, sunRotationSpeed);

        // Checks current rotation angle
        float currentSunAngle = sunParent.rotation.eulerAngles.z;
        if (currentSunAngle > 90 && currentSunAngle < 270) // Turn OFF
        {
            //print("OFF"); // Debug
            directionalLight.gameObject.SetActive(false);
        }
        else // Turn ON
        {
            //print("ON"); // Debug
            directionalLight.gameObject.SetActive(true);
        }
    }

    // Can be used to reset game time if the game is restarted
    void ResetGameTime()
    {
        gameTime = 0f;
    }
}