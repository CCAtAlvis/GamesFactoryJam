using UnityEngine;

public class BulletPistolScript : MonoBehaviour
{
    public float bulletSpeed;
    public float bulletDamage;
    public float TTL;

    // Update is called once per frame
    void Update ()
    {
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;
        TTL -= Time.deltaTime;

        if (TTL <= 0f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider other = collision.collider;
        Debug.Log(other.tag);

        if (other.tag == "Enemy" || other.tag == "Obstacle")
        {
            Destroy(this.gameObject);
        }
    }
}
