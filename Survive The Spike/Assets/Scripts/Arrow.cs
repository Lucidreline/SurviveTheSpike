using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    static int damage = 10;

    public void upgradeDamage(int upgradeBy) {
        damage += upgradeBy;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.collider.tag == "Enemies") {
            collision.collider.GetComponent<Enemy>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }

}
