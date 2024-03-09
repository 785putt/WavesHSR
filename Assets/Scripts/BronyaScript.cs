using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BronyaScript : MonoBehaviour
{
    public GameObject player;
    public Animator anim;
    public GameObject[] enemies;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Find all enemies
        enemies = GameObject.FindGameObjectsWithTag("Enemies");
        // Look at the player
        transform.LookAt(player.transform);
        if (Vector3.Distance(transform.position, player.transform.position) < 2)
        {
            anim.SetBool("isAttacking", true);
            anim.SetBool("isBuffing", false);
        }
        // If the enemies are near, buff the enemies
        else if (enemies.Length > 0)
        {
            foreach (GameObject enemy in enemies)
            {
                if (Vector3.Distance(transform.position, enemy.transform.position) < 15)
                {
                    // enemy.GetComponent<EnemyScript>().buffed = true;
                    anim.SetBool("isBuffing", true);
                    anim.SetBool("isAttacking", false);
                    Debug.Log("Buffing enemy");
                }
                else
                {
                    anim.SetBool("isBuffing", false);
                    anim.SetBool("isAttacking", false);
                }
            }
        }
        else
        {
            anim.SetBool("isAttacking", false);
            anim.SetBool("isBuffing", false);
        }
    }
}
