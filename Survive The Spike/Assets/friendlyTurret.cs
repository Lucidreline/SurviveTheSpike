using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class friendlyTurret : MonoBehaviour
{
    

    

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



    void Start()
    {
        
    }

    void Update() {
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

            Debug.Log("PEW PEW");
            timeToFire = fireRate + Time.time;  
        }
    }
    //if target = null then dont do any shooting, else search for target
    //if a target is found, put it in a variable and keep in in a while loop until he isnt in radius or he died

    //have a isTarget bool. if this bool is false then nothing will happen until it is true again

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
