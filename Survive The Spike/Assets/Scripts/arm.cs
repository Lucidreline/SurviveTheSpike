using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arm : MonoBehaviour
{
    bool attack;
    [SerializeField] Transform attackPos;
    [SerializeField] float attackRange;
    [SerializeField] LayerMask whatIsEnemy;
    [SerializeField] int damage = 50;
    [SerializeField] int damageRandomnessOffset = 10;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            attack = true;
            DealDamage();
        }
        Swing();
        attack = false;
    }

    void Swing() {
        if (attack) {
            gameObject.GetComponent<Animator>().SetTrigger("Attack");
        }      
    }

    void DealDamage() {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);
        for (int i = 0; i < enemiesToDamage.Length; i++) {
            int randDamage = damage + Random.Range(-damageRandomnessOffset, damageRandomnessOffset);
            enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(randDamage);
        }
    }
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
