using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enemyLogicEngine;
    private float maxHealth = 1000;
    public float currentHealth;
    private float currentAttack = 10;
    public TextMeshPro hptext;
    public GameObject hptxt;
    void Start()
    {
        currentHealth = maxHealth;
        enemyLogicEngine = GameObject.Find("EnemyLogicEngine");
        hptxt = GameObject.Find("hptext");
        hptext = hptxt.GetComponent<TextMeshPro>();
    }

    public void HandleButtonClick()
    {
        Debug.Log("Button is clicked");
    }



    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            Debug.Log("Player is dead");
            hptext.text = "RIP BOZO";
        }
        else
        {
            hptext.text = "HP " + currentHealth.ToString();
            // Debug.Log("Current health: " + currentHealth);
        }

        // Testing code to kill all enemies to check the animation
        if (Input.GetKey(KeyCode.K))
        {
            enemyLogicEngine.GetComponent<EnemyLogicEngineScript>().KillAllEnemies();
            Debug.Log("Key K pressed");
        }


    }

}
