﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    Transform player;
    bool latched = false;
    bool hitAnother = false;
    Vector3 latchOffset;

    playerMovement pMovement;

    void Start()
    {
        pMovement = FindObjectOfType<playerMovement>();
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.collider.tag == "Projectile" && !hitAnother) {
            hitAnother = true;
            Destroy(gameObject);
            return;
        // if a projectile hits another projectile that is on the player then it will be destroyed
        }
        if (!latched) {
            if (collision.collider.tag == "Player") {
                //Debug.Log("hit player");
                player = collision.collider.transform;
                gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                Destroy(gameObject.GetComponent<Rigidbody2D>());
                transform.SetParent(player.parent);

                FindObjectOfType<gameMaster>().Leach(5, 2, 0.5f, 0, false, transform);
                hitAnother = true;
                latched = true;
                return;
            }
        } 
    }

}

