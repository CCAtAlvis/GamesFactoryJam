using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float thresholdVisiblityAngle;
    public float thresholdVisiblityDistance;

    private float bulletPlayerAngle;
    private float bulletPlayerDistance;
    private GameObject player;
    private MeshRenderer bulletRenderer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        bulletRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        bulletPlayerAngle = Vector3.Angle(player.transform.forward, transform.forward);
        bulletPlayerDistance = Mathf.Abs(Vector3.Distance(player.transform.position, transform.position));

        if (bulletPlayerAngle <= thresholdVisiblityAngle && bulletPlayerDistance <= thresholdVisiblityDistance)
        {
            bulletRenderer.enabled = true;
        }
        else
        {
            bulletRenderer.enabled = false;
        }
        // -0.35    
    }
}
