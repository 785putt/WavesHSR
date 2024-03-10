using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyLogicEngineScript : MonoBehaviour
{
    public EnemyAttackState attackState;
    public EnemyHealthState healthState;
    private int enemyPerLevel = 10;
    private int eliteEnemyPerLevel = 1;
    private int currentLevel;
    private List<GameObject> enemies;
    public GameObject seele;
    public GameObject bronya;
    // Start is called before the first frame update
    void Start()
    {
        attackState = EnemyAttackState.attackNormal;
        healthState = EnemyHealthState.healthNormal;
        currentLevel = 1;
        // StartCoroutine(SpawnEnemies());
        enemies = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if all enemies are defeated
        StartCoroutine(CheckEnemiesStatus());
        // Debug.Log("Attack state: " + attackState);
        // Debug.Log("Health state: " + healthState);
    }
    // Coroutine to spawn enemies
    private IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < enemyPerLevel * currentLevel; i++)
        {
            // Spawn normal enemie
            enemies.Add(Instantiate(seele, new Vector3(Random.Range(-10, 10), 0.125f, Random.Range(-10, 10)), Quaternion.identity));
            // Instantiate(seele, new Vector3(Random.Range(-10, 10), 0.25f, Random.Range(-10, 10)), Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
        for (int i = 0; i < eliteEnemyPerLevel * currentLevel; i++)
        {
            // Spawn elite enemies
            enemies.Add(Instantiate(bronya, new Vector3(Random.Range(-10, 10), 0.125f, Random.Range(-10, 10)), Quaternion.identity));
            // Instantiate(bronya, new Vector3(Random.Range(-10, 10), 0.25f, Random.Range(-10, 10)), Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
        currentLevel++;
    }
    // Coroutine to check if all enemies are defeated
    private IEnumerator CheckEnemiesStatus()
    {
        yield return new WaitForSeconds(1);
        if (enemies.Count == 0)
        {
            StartCoroutine(SpawnEnemies());
        }
    }
}
