using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float enemyLife;
    public float enemyHitDamag;
    public float enemyHitDistance = 3f;

    public float thresholdVisiblityDistance;

    private GunController gunTrans;
    private SwordController swordTrans;

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
        gunTrans = playerCharacter.GetComponentInChildren<GunController>();
        swordTrans = playerCharacter.GetComponentInChildren<SwordController>();

        InvokeRepeating("hitPlayer", 3, 1);
    }

    void Update()
    {
        gunTrans = playerCharacter.GetComponentInChildren<GunController>();
        swordTrans = playerCharacter.GetComponentInChildren<SwordController>();

        if (gunTrans != null && gunTrans.isActiveAndEnabled)
            playerTransform = gunTrans.GetComponent<Transform>();
        else if (swordTrans != null && swordTrans.isActiveAndEnabled)
            playerTransform = swordTrans.GetComponent<Transform>();

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

        if (enemyLife <= 0f)
        {
            Debug.Log("enemy killed!");
            gameManager.enemyKilled();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider other = collision.collider;

        if (other.tag == "Bullet")
        {
            enemyLife -= other.GetComponent<BulletScript>().bulletDamage;
        }
        else if (other.tag == "PlayerGun" || other.tag == "PlayerSword")
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
        gunTrans = playerCharacter.GetComponentInChildren<GunController>();
        swordTrans = playerCharacter.GetComponentInChildren<SwordController>();

        if (gunTrans != null && gunTrans.isActiveAndEnabled)
            playerTransform = gunTrans.GetComponent<Transform>();
        else if (swordTrans != null && swordTrans.isActiveAndEnabled)
            playerTransform = swordTrans.GetComponent<Transform>();

        float distance = Vector3.Distance(playerTransform.position, transform.position);
        distance = Mathf.Abs(distance);

        if (distance < enemyHitDistance)
        {
            Debug.Log("player hit!");
            playerCharacter.GetComponent<PlayerContoller>().playerLife -= enemyHitDamag;
        }
    }
}
