using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] int damage = 10;

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.collider.tag == "Enemies") {
            collision.collider.GetComponent<Enemy>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }

}
