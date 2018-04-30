using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawnerScript : MonoBehaviour
{
    public GameObject enemyPrefab;
    private GameMamager gameManager;

    private GameObject[] enemySpawnPoints;
    private GameObject[] enemies;

    [SerializeField]
    public int enemyKilledThisLevel;
    [SerializeField]
    public int enemySpawnedThisLevel;
    [SerializeField]
    public int enemyCurrentlyInLevel;
    [SerializeField]
    public float enemyKillRatio;
    private float[] enemyKillRatioMin = { 0.1f, 0.14f, 0.17f, 0.2f, 0.25f, 0.3f, 0.36f, 0.43f, 0.50f };

    private int[] enemySpawnProbability = { 10, 20, 30, 40, 50, 60, 70, 80, 90 };
    private int[] enemySpawnProbabilityMin = { 10, 20, 30, 40, 50, 60, 70, 80, 90 };
    private int[] enemyLifeMax = { 10, 20, 30, 40, 50, 60, 70, 80, 90 };
    private int[] enemyDamageMax = { 10, 13, 16, 20, 25, 30, 36, 43, 50};
    private int[] enemyPresentMax = { 10, 10, 13, 16, 20, 22, 25, 27, 30};

    [SerializeField]
    private int levelNumber = 0;
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
        enemyKillRatio = 0.15f;
        enemyKilledThisLevel = 1;
        enemySpawnedThisLevel = 1;
        enemyCurrentlyInLevel = 0;
}

    void spawnEnemy()
    {
        if (enemyCurrentlyInLevel >= enemyPresentMax[levelNumber])
            return;

        probabilityOfSpawning = enemySpawnProbabilityMin[levelNumber] * Random.Range(0.01f, 0.5f) / enemyKillRatio;

        if (probabilityOfSpawning <= enemySpawnProbability[levelNumber])
        {
            int index = Random.Range(0, enemySpawnPoints.Length);
            Transform spawnPoint = enemySpawnPoints[index].transform;
            GameObject enemyClone;
            enemyClone = Instantiate(enemyPrefab, spawnPoint.position + spawnPoint.forward * 10, spawnPoint.rotation);

            EnemyController enemyController;
            enemyController = enemyClone.GetComponent<EnemyController>();
            enemyController.enemyLife = enemyLifeMax[levelNumber] * enemyKillRatio;
            enemyController.enemyHitDamag = enemyDamageMax[levelNumber] * enemyKillRatio;

            // Debug.Log("enemy spawned!");
            enemyCurrentlyInLevel++;
            enemySpawnedThisLevel++;
        }
    }

    public void changeLevel(int level)
    {
        Debug.Log("level changed, new lvl: "+level.ToString());
        levelNumber = level;
        enemySpawnPoints = null;
        enemySpawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawnPoint");

        enemyKillRatio = (float)enemyKilledThisLevel / enemySpawnedThisLevel;
        Debug.Log(enemyKilledThisLevel);
        enemyKilledThisLevel = 0;
        enemySpawnedThisLevel = 1;
        enemyCurrentlyInLevel = 0;

        if (enemyKillRatio < enemyKillRatioMin[level])
            enemyKillRatio = enemyKillRatioMin[level];
    }

    public void enemyDead()
    {
        Debug.Log("here");
        changeStats();
    }

    void changeStats()
    {
        enemyKilledThisLevel++;
        enemyCurrentlyInLevel--;
        Debug.Log(enemyCurrentlyInLevel);
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
