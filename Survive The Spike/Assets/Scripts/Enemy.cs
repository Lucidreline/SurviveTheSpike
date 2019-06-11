using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] RectTransform healthBarTransform;
    int health = 100;
    int maxHealth;
    [SerializeField] GameObject enemyExposion;
    [SerializeField] GameObject hitMarker;

    [SerializeField] int damageToPlayer = 3;
    [SerializeField] int damageToTurret = 10;

    bool explodeOnce = false;
    bool dead = false;
    //These bools are so stuff only gets called once and not twice by mistake


    [SerializeField] int coinReward = 3;

    private void Start() {
        maxHealth = health;
    }

    private void Update() {
        if(health <= 0 && !explodeOnce) {
            KilledByPlayer(3);
        }

        updateHealthBar();
    }


    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.collider.tag == "bow") {
            Debug.Log("BOOOOOOOWWW");
        }


        if (collision.collider.tag == "Player") {
            //Enemie died because it touched
            transform.parent.GetComponent<Animator>().SetTrigger("startSlowDeath");
            //Enemies are destroyed in the annimation

            if (!dead) {
                //FindObjectOfType<Player>().DamagePlayer(3);
                collision.collider.transform.parent.GetComponent<Player>().DamagePlayer(damageToPlayer);
                dead = true;
                FindObjectOfType<gameMaster>().AddToLiveEnemyCounter(-1);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Bow") {
            //Enemie died because it touched Bow
            transform.parent.GetComponent<Animator>().SetTrigger("startSlowDeath");
            //Enemies are destroyed in the annimation

            if (!dead) {
                collision.GetComponent<Bow>().DamageBow(damageToTurret);
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

    void updateHealthBar() {

        float healthPercent = (float)health / maxHealth;
        if (!dead) {
            healthBarTransform.localScale = new Vector3(healthPercent, healthBarTransform.localScale.y, healthBarTransform.localScale.z);
        }
        if (health <= 0) {
            Destroy(healthBarTransform.parent.gameObject);
        }
    }

    public void TakeDamage(int damage) {
        Debug.Log("Enemie took " + damage + " Damage");
        GameObject hitMarkerClone = Instantiate(hitMarker, transform.position, Quaternion.identity);
        Destroy(hitMarkerClone, 1);
        health -= damage;
    }
}
