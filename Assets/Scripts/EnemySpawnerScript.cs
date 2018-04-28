using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawnerScript : MonoBehaviour
{
    private GameObject enemyPrefab;
    private GameMamager gameManager;
    private int enemyCount;

    private GameObject[] enemySpawnPoints;
    private GameObject[] enemies;

    private int enemyKilledThisLevel = 1;
    private int enemySpawnedThisLevel = 1;

    [SerializeField]
    private float enemyKillRatio = 0.2f;
    private int[] enemyKillRatioMin;

    private int[] enemySpawnProbability = { 10};
    private int[] enemySpawnProbabilityMin = { 10};

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

    void spawnEnemy ()
    {
        probabilityOfSpawning = enemySpawnProbabilityMin[levelNumber] * Random.Range(0.1f, 1.0f) / enemyKillRatio;

        if (probabilityOfSpawning < enemySpawnProbability[levelNumber])
        {
            int index = Random.Range(0, enemySpawnPoints.Length);
            Transform spawnPoint = enemySpawnPoints[index].transform;
            GameObject enemyClone;
            enemyClone = Instantiate(enemyPrefab, spawnPoint.position + spawnPoint.forward * 10, spawnPoint.rotation);
        }
    }

    void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        levelNumber = 0;
        Debug.Log(scene);
        Debug.Log("hello");
        enemySpawnPoints = null;
        enemySpawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawnPoint");

        enemyKillRatio = (float)enemyKilledThisLevel / enemySpawnedThisLevel;
        enemyKilledThisLevel = 1;
        enemySpawnedThisLevel = 1;
    }
}
