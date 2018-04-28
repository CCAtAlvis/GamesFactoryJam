using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent enemyAgent;
    private Transform playerTransform;

    public float thresholdVisiblityAngle;
    public float thresholdVisiblityDistance;

    private float enemyPlayerAngle;
    private float enemyPlayerDistance;
    private MeshRenderer bulletRenderer;

    void Start()
    {
       /* playerCharacter = GameObject.FindGameObjectWithTag("Player");
        playerTransform = playerCharacter.GetComponent<Transform>(); */
        bulletRenderer = GetComponent<MeshRenderer>();
    }


    // Update is called once per frame
    void Update ()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        enemyAgent.SetDestination(playerTransform.position);

        //enemyPlayerAngle = Vector3.Angle(playerTransform.forward, transform.forward);
        enemyPlayerDistance = Mathf.Abs(Vector3.Distance(playerTransform.position, transform.position));

        if (/*enemyPlayerAngle <= thresholdVisiblityAngle && */enemyPlayerDistance <= thresholdVisiblityDistance)
        {
            bulletRenderer.enabled = true;
        }
        else
        {
            bulletRenderer.enabled = false;
        }

    }
}
