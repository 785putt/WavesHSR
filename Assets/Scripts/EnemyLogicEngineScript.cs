using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyLogicEngineScript : MonoBehaviour
{
    public EnemyAttackState attackState;
    public EnemyHealthState healthState;
    private int enemyPerLevel = 3;
    private int eliteEnemyPerLevel = 1;
    public int currentLevel;
    private int currentActiveEnemies;
    public List<GameObject> enemies;
    public GameObject seele;
    public GameObject bronya;
    public int score;
    public TextMeshPro leveltext;
    public TextMeshPro scoretext;
    public GameObject scoretxt;
    public GameObject leveltxt;
    public bool startSpawning = false;
    // Start is called before the first frame update
    void Start()
    {
        attackState = EnemyAttackState.attackNormal;
        healthState = EnemyHealthState.healthNormal;
        currentLevel = 1;
        // StartCoroutine(SpawnEnemies());
        enemies = new List<GameObject>();
        scoretxt = GameObject.Find("scoretext");
        leveltxt = GameObject.Find("leveltext");

        scoretext = scoretxt.GetComponent<TextMeshPro>();
        leveltext = leveltxt.GetComponent<TextMeshPro>();
    }

    // void OnEnable()
    // {
    //     scoretxt = GameObject.Find("scoretext");
    //     leveltxt = GameObject.Find("level");
    //     scoretxt.SetActive(true);
    //     leveltxt.SetActive(true);
    // }

    // Update is called once per frame
    void Update()
    {
        leveltext.text = "LEVEL " + currentLevel.ToString();
        scoretext.text = "KILLZ " + score.ToString();
        // Check if all enemies are defeated
        if (startSpawning == true)
        {
            StartCoroutine(CheckEnemiesStatus());
        }
        // Debug.Log("Current Active Enemies: " + enemies.Count);
        Debug.Log("Spawn state: " + startSpawning);
        // Debug.Log("Attack state: " + attackState);
        // Debug.Log("Health state: " + healthState);
    }
    // Coroutine to activate the old enemies (object pooling) before adding new enemies for the next level
    private IEnumerator ActivateEnemies()
    {
        yield return new WaitForSeconds(1);
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(true);
            // yield return new WaitForSeconds(0.5f);
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
            StartCoroutine(SpawnEnemies());
            StartCoroutine(ActivateEnemies());
        }
        currentActiveEnemies = 0;
    }

    // Function to kill all the enemies for testing purposes
    public void KillAllEnemies()
    {
        foreach (GameObject enemy in enemies)
        {
            // Debug.Log("Enemy name: " + enemy.name);
            if (enemy.name.Contains("Seele"))
            {
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
