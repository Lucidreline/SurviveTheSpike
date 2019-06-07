using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] Transform firePoint;

    [Header("Projectile")]
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed = 15;
    [SerializeField] float fireRate = 2;
    [SerializeField] float fireRateAddedRandomness = 2;

    void Start()
    {
        StartCoroutine(TurretFire(fireRate, fireRateAddedRandomness));

        if(projectile == null) {
            Debug.LogError("No reference to projectile from shooting script");
        }
    }

    IEnumerator TurretFire(float _fireRate, float _randomness) {
        float addRandom = Random.Range(0, _randomness);
        _fireRate += addRandom;
        //Adds randomness to the intervals between each shot

        yield return new WaitForSeconds(_fireRate);

        GameObject clone = Instantiate(projectile, firePoint.position, firePoint.rotation);
        clone.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(Vector2.down * (projectileSpeed * 100) * Time.deltaTime);
        //Spawns a projectile and then adds velocity to it.
        //NOTE: if the bullet is firing in a weird direction, change the "vector.down"

        StartCoroutine(TurretFire(fireRate, fireRateAddedRandomness));
        //Calls this method again, basicly looping
    }

}
