using UnityEngine;

public class GameMamager : MonoBehaviour
{
    public int levelNumber;
    public int killCountTotal;
    public float playerHealth;
    public GameObject enemyPrefab;

    // Use this for initialization
    void Start ()
    {
        DontDestroyOnLoad(gameObject);
	}
}
