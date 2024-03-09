using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeleScript : MonoBehaviour
{
    public GameObject player;
    public Animator anim;
    public GameObject enemyLogicEngine;
    private int baseDamage = 10;
    private int baseHealth = 100;
    [SerializeField] private int buffedDamage;
    [SerializeField] private int buffedHealth;
    [SerializeField] private int currentMaxHealth;
    [SerializeField] private int currentMaxDamage;
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
        // Look at the player
        transform.LookAt(player.transform);
        if (Vector3.Distance(transform.position, player.transform.position) < 2)
        {
            anim.SetBool("isAttacking", true);
        }
        else
        {
            anim.SetBool("isAttacking", false);
        }
        Debug.Log("Current damage: " + currentMaxDamage);
        Debug.Log("Current health: " + currentMaxHealth);
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
