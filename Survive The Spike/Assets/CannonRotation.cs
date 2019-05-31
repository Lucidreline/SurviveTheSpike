using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonRotation : MonoBehaviour
{
    [SerializeField] int rotationOffset = 0;
    Transform target;
    [SerializeField] bool isEnemyTurret;

    // Start is called before the first frame update
    void Awake()
    {
        if (isEnemyTurret) {
            target =  GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null) {
            Debug.Log("Cant find 'Player' tag!");
            return;
        }

        Vector3 difference = target.position - transform.position;
        difference.Normalize();     // makes all sum of vector = to 1;

        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg; //finding the angle in degrees
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ + rotationOffset);
    }
}
