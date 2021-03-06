﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSourceScript : MonoBehaviour
{
    [SerializeField]
    public int hitsToExplode;
    private GameMamager gameManager;

    void Start()
    {
        hitsToExplode = Random.Range(5, 11);
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameMamager>();
    }

    void Update()
    {
        if (hitsToExplode <= 0)
        {
            // Debug.Log("source collected");
            gameManager.collectLightSource();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider other = collision.collider;

        if (other.tag == "Bullet")
        {
            // Debug.Log("hit!");
            hitsToExplode--;
        }

        if (hitsToExplode <= 0)
        {
            // Debug.Log("source collected");
            gameManager.collectLightSource();
            Destroy(gameObject);
        }
    }
}
