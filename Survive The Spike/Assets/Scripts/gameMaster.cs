using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameMaster : MonoBehaviour
{
    
    

    public void Leach(int durration, int damageASec, float moveMultiply, int moveAdd, bool ispermanent, Transform projectile = null){
        StartCoroutine(FindObjectOfType<Player>().DamageLeach(2, durration));
        StartCoroutine(FindObjectOfType<playerMovement>().MovementLeach(moveMultiply, moveAdd, durration, ispermanent, projectile));
        
    }

    
}
