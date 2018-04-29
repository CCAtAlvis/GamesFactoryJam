using UnityEngine;
using UnityEngine.UI;

public class PlayerContoller : MonoBehaviour
{
    public float playerLife = 100f;
    private float playerLifeMax;

    public bool isGameRunning = true;

    public GameObject HUD;
    public Slider healthBar;

    public GameMamager gameManager;

    private bool isPlayerDead = false;


    public GunController gunCont;
    public SwordController swordCont;

    void Start()
    {
        playerLifeMax = playerLife;
    }

    void Update()
    {
        if (!isGameRunning)
            return;

        if (!isPlayerDead && playerLife <= 0f)
        {
            gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameMamager>();
            isPlayerDead = true;
            gameManager.playerDead();
            // Destroy(gameObject);
            HUD.SetActive(false);
            return;
        }

        float percentLife = playerLife / playerLifeMax;
        healthBar.value = percentLife;
    }

    public void addBullets(int count)
    {
        gunCont.bullets += 30;
    }
}
