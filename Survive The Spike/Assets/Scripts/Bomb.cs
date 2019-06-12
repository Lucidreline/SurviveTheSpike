using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] GameObject explosionPrefab;

    [SerializeField] LayerMask whatIsEnemy;
    [SerializeField] float ExplosionRange = 2;
    [SerializeField] int damage = 750;

    bool DamageDelt = false;
    
    void Explosion() {
        GameObject explosionClone = Instantiate(explosionPrefab, transform.position, transform.rotation);
        GetComponent<SpriteRenderer>().enabled = false;
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;

        if (!DamageDelt) {
            DoDamage();
        }


        Destroy(explosionClone, 3);
        Destroy(gameObject, 3);

    }

    void DoDamage() {


        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(transform.position, ExplosionRange, whatIsEnemy);
        for (int i = 0; i < enemiesToDamage.Length; i++) {
            Debug.Log("1");
            if(enemiesToDamage[i].tag == "Enemy Turret") {
                DamageDelt = true;
                enemiesToDamage[i].GetComponent<EnemyTurret>().DamageTurret(damage);
                Debug.Log("2");
            }
            else if(enemiesToDamage[i].tag == "Enemies") {
                DamageDelt = true;
                enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
            }
                
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ExplosionRange);
    }
}
