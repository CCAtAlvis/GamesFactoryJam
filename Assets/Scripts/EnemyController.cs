using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float enemyLife;
    public float enemyHitDamag;
    public float enemyHitDistance = 3f;

    public float thresholdVisiblityDistance;

    public GameObject gameManagerObject;
    private GameMamager gameManager;

    public NavMeshAgent enemyAgent;
    private Transform playerTransform;

    private float enemyPlayerAngle;
    private float enemyPlayerDistance;
    private MeshRenderer bulletRenderer;

    private GameObject playerCharacter;

    void Start()
    {
        bulletRenderer = GetComponent<MeshRenderer>();
        gameManager = gameManagerObject.GetComponent<GameMamager>();
        playerCharacter = GameObject.FindGameObjectWithTag("Player");

        InvokeRepeating("hitPlayer", 3, 1);
    }

    void Update()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        enemyAgent.SetDestination(playerTransform.position);

        enemyPlayerDistance = Mathf.Abs(Vector3.Distance(playerTransform.position, transform.position));

        if (enemyPlayerDistance <= thresholdVisiblityDistance)
        {
            bulletRenderer.enabled = true;
        }
        else
        {
            bulletRenderer.enabled = false;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider other = collision.collider;

        if (other.tag == "Bullet")
        {
            enemyLife -= other.GetComponent<BulletScript>().bulletDamage;
        }
        else if(other.tag == "Player")
        {
            other.GetComponent<PlayerContoller>().playerLife -= enemyHitDamag;
        }

        if (enemyLife <= 0f)
        {
            Debug.Log("enemy killed!");
            gameManager.enemyKilled();
            Destroy(gameObject);
        }
    }

    private void hitPlayer()
    {
        float distance = Vector3.Distance(playerCharacter.transform.position, transform.position);
        distance = Mathf.Abs(distance);
        
        if (distance < enemyHitDistance)
        {
            playerCharacter.GetComponent<PlayerContoller>().playerLife -= enemyHitDamag;
        }
    }
}
