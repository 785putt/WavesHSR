using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogicEngineScript : MonoBehaviour
{
    public EnemyAttackState attackState;
    public EnemyHealthState healthState;
    private int enemyPerLevel = 10;
    private int eliteEnemyPerLevel = 1;
    private int levelMultiplier = 2;
    private int currentLevel;
    public GameObject seele;
    public GameObject bronya;
    // Start is called before the first frame update
    void Start()
    {
        attackState = EnemyAttackState.attackNormal;
        healthState = EnemyHealthState.healthNormal;
        currentLevel = 1;
        SpawnEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("Attack state: " + attackState);
        // Debug.Log("Health state: " + healthState);
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < enemyPerLevel * currentLevel; i++)
        {
            // Spawn normal enemies
            Instantiate(seele, new Vector3(Random.Range(-10, 10), 0.25f, Random.Range(-10, 10)), Quaternion.identity);
        }
        for (int i = 0; i < eliteEnemyPerLevel * currentLevel; i++)
        {
            // Spawn elite enemies
            Instantiate(bronya, new Vector3(Random.Range(-10, 10), 0.25f, Random.Range(-10, 10)), Quaternion.identity);
        }
    }
}
