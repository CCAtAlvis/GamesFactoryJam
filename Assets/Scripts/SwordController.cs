using UnityEngine;

public class SwordController : MonoBehaviour
{
    private PlayerContoller playerCont;

    public float hitDamage;

    private void Start()
    {
        playerCont = GetComponentInParent<PlayerContoller>();
        hitDamage = 3f;
    }

    void Update()
    {
        if (!playerCont.isGameRunning)
            return;

        if (Input.GetButtonUp("Fire1"))
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2f);
            int i = 0;
            while (i < hitColliders.Length)
            {
                // Debug.Log(hitColliders[i].tag);
                switch(hitColliders[i].tag)
                {
                    case "Enemy":
                        hitColliders[i].GetComponent<EnemyController>().enemyLife -= hitDamage;
                        break;

                    case "LightSource":
                        LightSourceScript LSS = hitColliders[i].GetComponentInChildren<LightSourceScript>();
                        LSS.hitsToExplode--;
                        break;
                }
                i++;
            }
        }
    }
}
