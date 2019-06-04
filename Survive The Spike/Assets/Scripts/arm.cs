using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arm : MonoBehaviour
{
    bool attack;

    void Update()
    {
      
        if(Input.GetKeyDown(KeyCode.Mouse0))
            attack = true;

        Swing();
        attack = false;
    }

    void Swing() {
        if (attack) {
            Debug.Log("Called");
            gameObject.GetComponent<Animator>().SetTrigger("Attack");
        }
            
    }

    
}
