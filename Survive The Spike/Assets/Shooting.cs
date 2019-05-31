using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("Projectile")]
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed = 15;
    [SerializeField] float fireRate = 2;
    [SerializeField] float fireRateAddedRandomness = 2;

    [SerializeField] Transform firePoint;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TurretFire(fireRate, fireRateAddedRandomness));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator TurretFire(float _fireRate, float _randomness) {
        float addRandom = Random.Range(0, _randomness);
        _fireRate += addRandom;

        yield return new WaitForSeconds(_fireRate);

        GameObject clone = Instantiate(projectile, firePoint.position, firePoint.rotation);
        clone.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(Vector2.down * projectileSpeed);

        StartCoroutine(TurretFire(fireRate, fireRateAddedRandomness));
        
    }

}
