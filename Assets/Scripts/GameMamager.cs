using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMamager : MonoBehaviour
{
    public int levelNumber = 1;
    public int enemyKillCountTotal;
    public float playerHealth;

    public GameObject enemyPrefab;

    private PlayerContoller playerCont;
    private EnemySpawnerScript enemySpawner;

    private GameObject[] lightSources;
    private int lightSourcesCollected = 0;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        enemySpawner = gameObject.GetComponent<EnemySpawnerScript>();
        playerCont = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerContoller>();
    }

    private void OnLevelWasLoaded(int level)
    {
        lightSources = null;
        lightSources = GameObject.FindGameObjectsWithTag("LightSource");
    }

    public void enemyKilled()
    {
        enemySpawner = GetComponent<EnemySpawnerScript>();
        enemyKillCountTotal++;
        enemySpawner.enemyDead();
    }

    public void playerDead()
    {
        // Debug.Log("player dead!");

        StartCoroutine(LoadLevelAsync(4));
    }

    public void changeLevel()
    {
        Debug.Log("level complete!");
        levelNumber++;
        StartCoroutine(LoadLevelAsync(levelNumber));
    }

    public void collectLightSource()
    {
        lightSourcesCollected++;

        if (lightSourcesCollected == lightSources.Length)
        {
            Debug.Log("max light source reached");
            changeLevel();
        }
    }

    IEnumerator LoadLevelAsync(int levelIndex)
    {
        Debug.Log("destroying all objects");
        foreach (GameObject go in GameObject.FindObjectsOfType<GameObject>())
        {
            if (go.GetComponent<GameMamager>() == null)
            {
                Destroy(go);
            }
        }

        yield return new WaitForSecondsRealtime(10f);

        Debug.Log("loading new level, with level index: " + levelIndex.ToString());
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);

        while (!operation.isDone)
        {
            yield return null;
        }

    }
}
