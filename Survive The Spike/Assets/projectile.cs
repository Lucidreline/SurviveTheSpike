using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    Transform player;
    bool latched = false;
    Vector3 latchOffset;

    playerMovement pMovement;

    // Start is called before the first frame update
    void Start()
    {
        pMovement = FindObjectOfType<playerMovement>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (!latched) {
            if (collision.collider.tag == "Player") {
                player = collision.collider.transform;
                gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                Destroy(gameObject.GetComponent<Rigidbody2D>());
                transform.SetParent(player.parent);
                StartCoroutine(pMovement.alterMoveSpeed(.1f, -1, 5, false, transform));
                latched = true;
                return;
            }
        }
        
    }

}

