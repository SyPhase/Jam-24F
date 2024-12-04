using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tower : MonoBehaviour
{
    [SerializeField] int health = 100;
    [Tooltip("(Canvas) UI health text")]
    [SerializeField] TMP_Text healthText;

    void OnTriggerEnter(Collider other)
    {
        health--;
        healthText.text = "HP: " + health;
        Destroy(other.gameObject); // Destroy enemy

        if (health <= 0)
        {
            healthText.text = "GAME OVER";

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload this scene
        }
    }
}
