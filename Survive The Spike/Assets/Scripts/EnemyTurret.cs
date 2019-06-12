using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
    [SerializeField] int health = 750;

    [Header("Explosion")]
    [SerializeField] GameObject fireExplosion;
    [SerializeField] GameObject cactusExplosion;

    bool hasBeenDestroyed = false;

    private void Update() {
        if(health <= 0 && !hasBeenDestroyed) {
            DestroyTurret();
        }
    }

    public void DamageTurret(int damage) {
        health -= damage;
    }

    void DestroyTurret() {
        GameObject fireExplosionClone = Instantiate(fireExplosion, transform.position, transform.rotation);
        GameObject cactusExplosionClone = Instantiate(cactusExplosion, transform.position, transform.rotation);

        Destroy(fireExplosionClone, 3);
        Destroy(cactusExplosionClone, 3);
        Destroy(gameObject);

        hasBeenDestroyed = true;
    }
}
