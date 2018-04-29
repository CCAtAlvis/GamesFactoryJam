using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMamager : MonoBehaviour
{
    public int levelNumber = 0;
    public int enemyKillCountTotal;
    public float playerHealth;
    private bool isGameRunning = true;

    public GameObject enemyPrefab;

    private PlayerContoller playerCont;
    private EnemySpawnerScript enemySpawner;

    private int lightSourcesCollected = 0;

    public GameObject PauseMenu;
    private bool isShowingPauseMenu = false;
    public GameObject LevelOverMenu;
    public GameObject PlayerDeadMenu;
    public GameObject GameComplete;

    public Text HUDLevelDisplay;

    public GameObject lightSourcePrefab;
    private int[] numberOfLightSource = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    private Vector3[,] lightSourceLocation = new Vector3[10, 10];
    // i: level number starting from 0
    // j: positions of light source

    void Awake()
    {
        Time.timeScale = 1;
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

        // light source location init
        // level 2
        lightSourceLocation[1, 0] = new Vector3(0, 0, 0);
        lightSourceLocation[1, 1] = new Vector3(10, 0, 10);

        // level 3
        lightSourceLocation[2, 0] = new Vector3(3 , 0, 3);
        lightSourceLocation[2, 1] = new Vector3(-10, 0, 10);
        lightSourceLocation[2, 2] = new Vector3(20, 0, -10);

        // level 4
        lightSourceLocation[3, 0] = new Vector3(0, 0, 0);
        lightSourceLocation[3, 1] = new Vector3(10, 0, 10);
        lightSourceLocation[3, 2] = new Vector3(20, 0, 10);
        lightSourceLocation[3, 3] = new Vector3(20, 0, 20);
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            isShowingPauseMenu = !isShowingPauseMenu;
            PauseMenu.SetActive(isShowingPauseMenu);
            playerCont.isGameRunning = !isShowingPauseMenu;

            if (isShowingPauseMenu)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }

        /*if (Input.GetKeyDown(KeyCode.Q) )
        {
            // Debug.Log("showing level over menu");
            PlayerDeadMenu.SetActive(true);
        }*/
    }

    public void PauseMenuHandler(int option)
    {
        playerCont = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerContoller>();

        switch (option)
        {
            // back to game
            case 0:
                isShowingPauseMenu = !isShowingPauseMenu;
                PauseMenu.SetActive(isShowingPauseMenu);
                playerCont.isGameRunning = true;
                Time.timeScale = 1;
                break;

            // exit game
            case 1:
                Application.Quit();
                break;

            // start again
            case 2:
                SceneManager.LoadScene(0);
                break;

            // continue
            case 3:
                foreach (GameObject go in GameObject.FindGameObjectsWithTag("LightSource"))
                {
                    Destroy(go);
                }

                LevelOverMenu.SetActive(false);
                HUDLevelDisplay.text = "Level: " + (levelNumber + 1).ToString();
                playerCont.isGameRunning = true;
                Time.timeScale = 1;
                InitLevel(levelNumber);
                break;
        }
    }







    public void enemyKilled()
    {
        enemyKillCountTotal++;
        enemySpawner = GetComponent<EnemySpawnerScript>();
        enemySpawner.enemyDead();
    }







    public void playerDead()
    {
        Debug.Log("player dead!");
        Time.timeScale = 0;
        PlayerDeadMenu.SetActive(true);
    }







    public void collectLightSource()
    {
        lightSourcesCollected++;
        Debug.Log(numberOfLightSource[levelNumber]);

        if (lightSourcesCollected == numberOfLightSource[levelNumber])
        {
            Debug.Log("max light source reached");
            changeLevel();
        }
    }

    public void changeLevel()
    {
        Debug.Log("level complete!");
        playerCont = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerContoller>();
        playerCont.isGameRunning = false;
        playerCont.bullets += 30;
        Time.timeScale = 0;
        levelNumber++;
        lightSourcesCollected = 0;

        if (levelNumber >= 4)
        {
            GameComplete.SetActive(true);
            return;
        }

        StartCoroutine(LoadNewLevel(levelNumber));
    }

    IEnumerator LoadNewLevel(int level)
    {
        Debug.Log("destroying all objects");
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(go);
        }

        LevelOverMenu.SetActive(true);
        Debug.Log("loading new level, with level index: " + level.ToString());

        yield return null;
    }

    private void InitLevel(int level)
    {
        // spawn whatever is required
        for (int i = 0; i < numberOfLightSource[level]; ++i)
        {
            Instantiate(lightSourcePrefab, lightSourceLocation[level, i], Quaternion.identity);
        }
    }
}
