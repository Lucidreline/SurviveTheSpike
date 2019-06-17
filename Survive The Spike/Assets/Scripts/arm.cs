using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arm : MonoBehaviour
{
    bool attack;
    [SerializeField] Transform attackPos;
    [SerializeField] float attackRange;
    [SerializeField] LayerMask whatIsEnemy;
    public int attackDamage = 50;
    [SerializeField] int damageRandomnessOffset = 10;

    void Update()
    {
        
        Swing();
        attack = false;
    }

    void Swing() {
        if (attack) {
            gameObject.GetComponent<Animator>().SetTrigger("Attack");
        }      
    }

    public void DealDamage() {
        attack = true;
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);
        for (int i = 0; i < enemiesToDamage.Length; i++) {
            int randDamage = attackDamage + Random.Range(-damageRandomnessOffset, damageRandomnessOffset);
            enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(randDamage);
        }
    }
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
