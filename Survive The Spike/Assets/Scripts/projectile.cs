using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    Transform player;
    bool latched = false;
    bool hitAnother = false;
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
        if(collision.collider.tag == "Projectile" && !hitAnother) {
            hitAnother = true;
            Destroy(gameObject);
            Debug.Log("Destroy because hit another");
            return;
        }
        if (!latched) {
            if (collision.collider.tag == "Player") {
                Debug.Log("hit player");
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

