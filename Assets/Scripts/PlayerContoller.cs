using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    public float speedMovement = 4f;
    public float speedRotation = 4f;
    public float speedMultipler = 1f;
    public float playerLife = 100f;
    public Transform bulletSpawnPoint;
    public Transform ParentSpawn;

    public GameObject bulletPistol;
    private GameMamager gameManager;

    private bool isPlayerDead = false;

	void Update()
    {
        if (!isPlayerDead && playerLife <= 0f)
        {
            gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameMamager>();
            isPlayerDead = true;
            gameManager.playerDead();
            Destroy(gameObject);
        }

        if (Input.GetButtonUp("Fire1"))
        {
            Instantiate(bulletPistol, bulletSpawnPoint.position + bulletSpawnPoint.forward, bulletSpawnPoint.rotation);
        }

        // movement: forward
		if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * speedMovement * speedMultipler * Time.deltaTime;
        }

        // movement: backwards
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * speedMovement * speedMultipler * Time.deltaTime;
        }

        // movement: left
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * speedMovement * speedMultipler * Time.deltaTime;
        }

        // movement: backwards
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * speedMovement * speedMultipler * Time.deltaTime;
        }

        // enable running 
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speedMultipler = 3f;
        }
        else
        {
            speedMultipler = 1f;
        }

        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitdist = 0.0f;

        if (playerPlane.Raycast(ray, out hitdist))
        {
            Vector3 targetPoint = ray.GetPoint(hitdist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speedRotation* Time.deltaTime);
        }
    }
}
