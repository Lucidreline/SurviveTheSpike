using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusIndicator : MonoBehaviour
{
    float parentZRot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        parentZRot = transform.parent.transform.rotation.z;
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, -parentZRot);
    }
}
