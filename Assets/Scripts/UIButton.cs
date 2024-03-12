using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButton : MonoBehaviour
{
    public PlayerScript playerScript;
    public Transform playerTransform;
    public float detectionRadius = 1f;
    public GameObject uiButtonInstance; // Instance of the UI button
    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.FindObjectOfType<PlayerScript>();
        uiButtonInstance = GameObject.Find("TextButton");
    }

    void update()
    {
        if (uiButtonInstance != null)
        {
            // Check the distance between the player and the object
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

            // If the player is within the detection radius and the trigger has not been set yet
            if (distanceToPlayer <= detectionRadius)
            {
                // Set IsNear trigger and log to console
                Debug.Log("Player is near the object!");

            }
            if (distanceToPlayer > detectionRadius)
            {
                // If the player is outside the detection radius, reset the trigger and reset the flag
                Debug.Log("Player is near the object!");
            }

        }
    }

    public void OnButtonClick()
    {
        // Call a method in PlayerScript when the button is clicked
        playerScript.HandleButtonClick();
    }
}
