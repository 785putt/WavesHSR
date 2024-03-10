using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class SeeleScript : MonoBehaviour
{
    public GameObject player;
    private float distanceToPlayer;
    public Animator anim;
    public GameObject enemyLogicEngine;
    private int baseDamage = 10;
    private int baseHealth = 100;
    private float attackSpeed = 2.6f;
    private bool isAttacking = false;
    [SerializeField] private int buffedDamage;
    [SerializeField] private int buffedHealth;
    [SerializeField] private int currentMaxHealth;
    [SerializeField] private int currentMaxDamage;
    private int currentHealth;
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
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = GetComponentInParent<EnemyProximityCheckerScript>().distanceToPlayer;
        // Coroutine to check the buffed state
        StartCoroutine(CheckBuffedState());
        if (distanceToPlayer < 1.5f)
        {
            anim.SetBool("isAttacking", true);
            // To make sure that the coroutine is not running multiple times
            if (isAttacking == false)
            {
                StartCoroutine(CheckIfInRange());
            }
        }
        else
        {
            // Look at the player
            transform.parent.LookAt(player.transform);
            anim.SetBool("isAttacking", false);
        }
        // Debug.Log("Current damage: " + currentMaxDamage);
        // Debug.Log("Current health: " + currentMaxHealth);
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
            Debug.Log("Hit player for " + currentMaxDamage + " damage");
            isAttacking = false;
        }
        else
        {
            yield return null;
        }
    }
    // // Deals damage to the player if they are inside the trigger with the attack speed delay
    // private void OnTriggerStay(Collider other)
    // {
    //     if (other.gameObject.tag == "Player")
    //     {
    //         other.gameObject.GetComponent<PlayerScript>().currentHealth -= currentMaxDamage;
    //         Debug.Log("Hit player for " + currentMaxDamage + " damage");
    //         StartCoroutine(AttackSpeedDelay());
    //     }
    // }
    // // Coroutine to delay the attack speed
    // private IEnumerator AttackSpeedDelay()
    // {
    //     yield return new WaitForSeconds(attackSpeed);
    // }
    // Fix the rotation from the lookAt method since sometimes it goes upside down and goes through the floor
    private void LateUpdate()
    {
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
    }
}
