using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    int enemyHealth = 100;
    [SerializeField] GameObject enemyExposion;
    [SerializeField] GameObject hitMarker;

    bool explodeOnce = false;
    bool dead = false;
    //These bools are so stuff only gets called once and not twice by mistake


    [SerializeField] int coinReward = 3;

    private void Update() {
        if(enemyHealth <= 0 && !explodeOnce) {
            KilledByPlayer(3);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.collider.tag == "Player") {
            //Enemie died because it touched
            transform.parent.GetComponent<Animator>().SetTrigger("startSlowDeath");
            FindObjectOfType<Player>().DamagePlayer(3);
            if (!dead) {
                dead = true;
                FindObjectOfType<gameMaster>().AddToLiveEnemyCounter(-1);
            }
        }
    }

    void KilledByPlayer(int coinReward) {
        //Enemie died because player killed him
        explodeOnce = true;
        GameObject explosionClone = Instantiate(enemyExposion, transform.position, Quaternion.identity);
        FindObjectOfType<gameMaster>().AddToLiveEnemyCounter(-1);
        FindObjectOfType<gameMaster>().addCoins(coinReward);
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
