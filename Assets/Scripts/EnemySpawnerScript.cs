using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawnerScript : MonoBehaviour
{
    private GameObject enemyPrefab;
    private GameMamager gameManager;

    private GameObject[] enemySpawnPoints;
    private GameObject[] enemies;

    private int enemyKilledTotal = 0;
    private int enemySpawnedTotal = 0;
    private float killRatioTotal = 1;

    private int enemyKilledThisLevel = 1;
    private int enemySpawnedThisLevel = 1;
    private int enemyCurrentlyInLevel = 0;

    private float enemyKillRatio = 0.15f;
    private int[] enemyKillRatioMin;

    private int[] enemySpawnProbability = { 10 };
    private int[] enemySpawnProbabilityMin = { 10 };
    private int[] enemyLifeMin = { 10 };
    private int[] enemyDamageMin = { 10 };
    private int[] enemyMaxPresent = { 10 };

    private int levelNumber;
    private float probabilityOfSpawning;

    void Awake()
    {
        gameManager = GetComponent<GameMamager>();
        enemyPrefab = gameManager.enemyPrefab;
        enemySpawnPoints = null;
        enemySpawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawnPoint");
    }

    void Start()
    {
        InvokeRepeating("spawnEnemy", 1f, 0.1f);
    }

    void spawnEnemy()
    {
        if (enemyCurrentlyInLevel > enemyMaxPresent[levelNumber])
            return;

        probabilityOfSpawning = enemySpawnProbabilityMin[levelNumber] * Random.Range(0.1f, 1.0f) / enemyKillRatio;

        if (probabilityOfSpawning < enemySpawnProbability[levelNumber])
        {
            int index = Random.Range(0, enemySpawnPoints.Length);
            Transform spawnPoint = enemySpawnPoints[index].transform;
            GameObject enemyClone;
            enemyClone = Instantiate(enemyPrefab, spawnPoint.position + spawnPoint.forward * 10, spawnPoint.rotation);

            EnemyController enemyController;
            enemyController = enemyClone.GetComponent<EnemyController>();
            enemyController.enemyLife = enemyLifeMin[levelNumber] * enemyKillRatio;
            enemyController.enemyHitDamag = enemyDamageMin[levelNumber] * enemyKillRatio;

            // Debug.Log("enemy spawned!");
            enemyCurrentlyInLevel++;
            enemySpawnedThisLevel++;
            enemySpawnedTotal++;
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        // Debug.Log("in here");
        levelNumber = level - 1;
        enemySpawnPoints = null;
        enemySpawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawnPoint");

        enemyKillRatio = (float)enemyKilledThisLevel / enemySpawnedThisLevel;
        enemyKilledThisLevel = 1;
        enemySpawnedThisLevel = 1;

        killRatioTotal = (float)enemyKilledTotal / enemySpawnedTotal;
    }

    public void enemyDead()
    {
        // Debug.Log("here");
        enemyKilledThisLevel++;
        enemyKilledTotal++;
        enemyCurrentlyInLevel--;
    }

    public int getEnemyKill()
    {
        return enemyKilledThisLevel;
    }

    public float getKillRatio()
    {
        return enemyKillRatio;
    }
}
