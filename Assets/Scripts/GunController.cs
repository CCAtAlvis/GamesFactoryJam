using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPistol;

    public Text bulletDisplay;
    public int bullets = 20;

    private PlayerContoller playerCont;

    private void Start()
    {
        playerCont = GetComponentInParent<PlayerContoller>();
    }

    void Update()
    {
        if (!playerCont.isGameRunning)
            return;

        if (Input.GetButtonUp("Fire1") && bullets > 0)
        {
            bullets--;
            Instantiate(bulletPistol, bulletSpawnPoint.position + bulletSpawnPoint.forward, bulletSpawnPoint.rotation);
        }

        bulletDisplay.text = "Bullets: " + bullets.ToString();
    }
}
