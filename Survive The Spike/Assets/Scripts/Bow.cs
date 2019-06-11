using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{

    [Header("Stats")]
    [SerializeField] int health = 100;
    int maxHealth;
    [SerializeField] RectTransform healthBarTransform;

    [Header("Aiming")]
    [SerializeField] Transform target;
    float targetDistance;
    bool currentTarget = false;

    [SerializeField] float rotOffset = -90;

    [SerializeField] float attackRange = 5;
    [SerializeField] LayerMask whatIsEnemies;


    [Header("Shooting")]
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] float arrowSpeed = 10f;
    [SerializeField] Transform firePoint;

    [SerializeField] float fireRate = .5f;
    float timeToFire = 0;

    private void Start() {
        maxHealth = health;
    }

    public void DamageBow(int damage) {
        health -= damage;
    }

    void BowDeath() {
        Destroy(gameObject);
        //make this cooler
    }


    void Update() {
        updateHealthBar();

        if (health <= 0)
            BowDeath();


        if (!currentTarget) {
            SearchForTarget();
        } else {
            if (target == null)
                currentTarget = false;
            calcTargetDistance();
            RotateToTarget();
            FireArrows();
            
        }
    }

    void SearchForTarget() {
        Collider2D[] thingsToAttack = Physics2D.OverlapCircleAll(transform.position, attackRange, whatIsEnemies);
        if (thingsToAttack.Length > 0) {
            target = thingsToAttack[0].transform;
            currentTarget = true;

        }
    }

    private void RotateToTarget() {
        Vector3 difference = target.position - transform.position;
        difference.Normalize();     // makes all sum of vector = to 1;

        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg; //finding the angle in degrees
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ + rotOffset);
    }

    void calcTargetDistance() {
        targetDistance = Vector3.Distance(target.position, transform.position);
        if(targetDistance > attackRange) {
            currentTarget = false;
        }
    }
    
    void FireArrows() {
        if (Time.time > timeToFire) {

            GameObject ArrowClone = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
            ArrowClone.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(Vector2.up * (arrowSpeed * 100) * Time.deltaTime);

            timeToFire = fireRate + Time.time;  
        }
    }

    void updateHealthBar() {
        
        float healthPercent = (float)health / maxHealth;
        healthBarTransform.localScale = new Vector3(healthPercent, healthBarTransform.localScale.y, healthBarTransform.localScale.z);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
