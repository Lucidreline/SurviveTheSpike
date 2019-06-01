using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonRotation : MonoBehaviour
{
    Transform target;
    [SerializeField] string targetTag = "Player";
    [SerializeField] int rotationOffset = 0;
    //Idealy, the Cannon should point to the right by default... but if its not then you would add some offset.
    [SerializeField] bool isEnemyTurret;

    void Awake()
    {
        if (isEnemyTurret)
            target =  GameObject.FindGameObjectWithTag(targetTag).transform;
    }

    void Update()
    {
        if(target == null) {
            Debug.Log("Can't Find game object with the tag: " + targetTag);
            return;
        }

        Vector3 difference = target.position - transform.position;
        difference.Normalize();     // makes all sum of vector = to 1;

        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg; //finding the angle in degrees
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ + rotationOffset);
    }
}
