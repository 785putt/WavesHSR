using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BronyaScript : MonoBehaviour
{
    public GameObject player;
    public Animator anim;
    public GameObject enemyLogicEngine;
    public GameObject[] enemies;
    [SerializeField] private int baseDamage = 20;
    [SerializeField] private int baseHealth = 150;
    [SerializeField] private int buffedDamage;
    [SerializeField] private int buffedHealth;
    private int currentMaxHealth;
    private int currentMaxDamage;
    private int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        enemyLogicEngine = GameObject.Find("EnemyLogicEngine");
        player = GameObject.Find("PlayerCapsule");
        currentMaxHealth = baseHealth;
        currentMaxDamage = baseDamage;
        buffedDamage = baseDamage * 2;
        buffedHealth = baseHealth * 2;
    }

    // Update is called once per frame
    void Update()
    {
        // Coroutine to check the buffed state
        StartCoroutine(CheckBuffedState());
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
            anim.SetBool("isAttacking", false);
            anim.SetBool("isBuffing", false);
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
}
