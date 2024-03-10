using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BronyaScript : MonoBehaviour
{
    public GameObject player;
    private float distanceToPlayer;
    public Animator anim;
    public GameObject enemyLogicEngine;
    public GameObject[] enemies;
    [SerializeField] private int baseDamage = 20;
    [SerializeField] private int baseHealth = 150;
    [SerializeField] private int buffedDamage;
    [SerializeField] private int buffedHealth;
    private float attackSpeed = 3.4f;
    private bool isAttacking = false;
    private bool isDead = false;
    private int currentMaxHealth;
    private int currentMaxDamage;
    public int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInParent<Animator>();
        enemyLogicEngine = GameObject.Find("EnemyLogicEngine");
        player = GameObject.Find("Professor Herta");
        currentMaxHealth = baseHealth;
        currentMaxDamage = baseDamage;
        buffedDamage = baseDamage * 2;
        buffedHealth = baseHealth * 2;
        currentHealth = currentMaxHealth;
        anim.SetBool("isDead", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth > 0)
        {
            distanceToPlayer = GetComponentInParent<EnemyProximityCheckerScript>().distanceToPlayer;
            // Coroutine to check the buffed state
            StartCoroutine(CheckBuffedState());
            // Find all enemies
            enemies = GameObject.FindGameObjectsWithTag("Enemies");
            if (distanceToPlayer < 1.5)
            {
                anim.SetBool("isAttacking", true);
                anim.SetBool("isBuffing", false);
                // To make sure that the coroutine is not running multiple times
                if (isAttacking == false)
                {
                    StartCoroutine(CheckIfInRange());
                }
            }
            // If the enemies are near, buff the enemies
            else if (enemies.Length > 0)
            {
                foreach (GameObject enemy in enemies)
                {
                    if (distanceToPlayer < 15)
                    {
                        // Set the attack state to buffed
                        enemyLogicEngine.GetComponent<EnemyLogicEngineScript>().attackState = EnemyAttackState.attackBuffed;
                        anim.SetBool("isBuffing", true);
                        anim.SetBool("isAttacking", false);
                        // Debug.Log("Buffing enemy");
                        break;
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
                // Look at the player
                transform.parent.LookAt(player.transform);
                anim.SetBool("isAttacking", false);
                anim.SetBool("isBuffing", false);
            }
        }
        else if (isDead == false && currentHealth <= 0)
        {
            isDead = true;
            anim.SetBool("isDead", true);
            Debug.Log("Bronya is dead");
        }
    }
    private IEnumerator CheckBuffedState()
    {
        if (enemyLogicEngine.GetComponent<EnemyLogicEngineScript>().attackState == EnemyAttackState.attackBuffed)
        {
            currentMaxDamage = buffedDamage;
            yield return null;
        }
        else if (enemyLogicEngine.GetComponent<EnemyLogicEngineScript>().attackState == EnemyAttackState.attackNormal)
        {
            currentMaxDamage = baseDamage;
            yield return null;
        }
        else if (enemyLogicEngine.GetComponent<EnemyLogicEngineScript>().healthState == EnemyHealthState.healthBuffed)
        {
            currentMaxHealth = buffedHealth;
            yield return null;
        }
        else if (enemyLogicEngine.GetComponent<EnemyLogicEngineScript>().healthState == EnemyHealthState.healthNormal)
        {
            currentMaxHealth = baseHealth;
            yield return null;
        }
    }
    // Deals damage to the player after the attack animation when the player is within range
    private IEnumerator CheckIfInRange()
    {
        distanceToPlayer = GetComponentInParent<EnemyProximityCheckerScript>().distanceToPlayer;
        if (distanceToPlayer < 1.5f)
        {
            isAttacking = true;
            yield return new WaitForSeconds(attackSpeed);
            player.GetComponent<PlayerScript>().currentHealth -= currentMaxDamage;
            // Debug.Log("Hit player for " + currentMaxDamage + " damage");
            isAttacking = false;
        }
        else
        {
            yield return null;
        }
    }

    // Fix the rotation from the lookAt method since sometimes it goes upside down and goes through the floor
    private void LateUpdate()
    {
        transform.parent.rotation = Quaternion.Euler(0, transform.parent.eulerAngles.y, 0);
    }
}
