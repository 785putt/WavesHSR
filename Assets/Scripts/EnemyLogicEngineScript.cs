using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyLogicEngineScript : MonoBehaviour
{
    public EnemyAttackState attackState;
    public EnemyHealthState healthState;
    private int enemyPerLevel = 3;
    private int eliteEnemyPerLevel = 1;
    private int currentLevel;
    private int currentActiveEnemies;
    public List<GameObject> enemies;
    public GameObject seele;
    public GameObject bronya;
    public int score;
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
        Debug.Log("Score: " + score);
        // Debug.Log("Attack state: " + attackState);
        // Debug.Log("Health state: " + healthState);
    }
    // Coroutine to activate the old enemies (object pooling) before adding new enemies for the next level
    private IEnumerator ActivateEnemies()
    {
        // yield return new WaitForSeconds(1);
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }
    }
    // Coroutine to spawn enemies
    private IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < enemyPerLevel; i++)
        {
            // Spawn normal enemies
            enemies.Add(Instantiate(seele, new Vector3(Random.Range(-10, 10), 0.125f, Random.Range(-10, 10)), Quaternion.identity));
            // Instantiate(seele, new Vector3(Random.Range(-10, 10), 0.25f, Random.Range(-10, 10)), Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
        for (int i = 0; i < eliteEnemyPerLevel; i++)
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
        foreach (GameObject enemy in enemies)
        {
            // Check the enemies active state
            if (enemy.activeSelf == true)
            {
                currentActiveEnemies++;
            }
        }
        if (currentActiveEnemies == 0)
        {
            // Debug.Log("All enemies are defeated");
            StartCoroutine(ActivateEnemies());
            StartCoroutine(SpawnEnemies());
        }
        currentActiveEnemies = 0;
    }

    // Function to kill all the enemies for testing purposes
    public void KillAllEnemies()
    {
        foreach (GameObject enemy in enemies)
        {
            // Debug.Log("Enemy name: " + enemy.name);
            if (enemy.name.Contains("Seele")){
                Debug.Log("Seele HP: " + enemy.GetComponentInChildren<SeeleScript>().currentHealth);
                enemy.GetComponentInChildren<SeeleScript>().currentHealth = 0;
            }
            else if (enemy.name.Contains("Bronya"))
            {
                Debug.Log("Bronya HP: " + enemy.GetComponentInChildren<BronyaScript>().currentHealth);
                enemy.GetComponentInChildren<BronyaScript>().currentHealth = 0;
            }
            // Destroy(enemy);
        }
        // enemies.Clear();
    }
}
