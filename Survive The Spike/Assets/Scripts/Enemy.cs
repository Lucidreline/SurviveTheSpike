using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    int enemyHealth = 100;
    [SerializeField] GameObject enemyExposion;
    [SerializeField] GameObject hitMarker;
    bool explodeOnce = false;

    [SerializeField] int coinReward = 3;

    private void Update() {
        if(enemyHealth <= 0 && !explodeOnce) {
            KilledByPlayer(3);
            FindObjectOfType<gameMaster>().addCoins(coinReward);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.collider.tag == "Player") {
            transform.parent.GetComponent<Animator>().SetTrigger("startSlowDeath");
            Debug.Log("Enemie died because it touched");
            FindObjectOfType<Player>().DamagePlayer(3);
            
        }
    }

    void KilledByPlayer(int coinReward) {
        explodeOnce = true;
        GameObject explosionClone = Instantiate(enemyExposion, transform.position, Quaternion.identity);
        Debug.Log("Enemie died because of player");

        Destroy(explosionClone, .2f);
        Destroy(transform.parent.gameObject, .1f);
    }

    public void TakeDamage(int damage) {
        Debug.Log("Enemie took " + damage + " Damage");
        GameObject hitMarkerClone = Instantiate(hitMarker, transform.position, Quaternion.identity);
        Destroy(hitMarkerClone, 1);
        enemyHealth -= damage;
    }
}
