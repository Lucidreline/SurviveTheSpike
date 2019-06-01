using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    [SerializeField] Transform target;

    // I added a smooth damp on the camera movement at first but then liked how it looked without.
    // Since the player movement has a smooth damp already it doesn't look bad at all.

    private void Start() {
        if(target == null) {
            Debug.LogError("Can not find reference to target");
        }
    }
    void Update(){
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }
}
