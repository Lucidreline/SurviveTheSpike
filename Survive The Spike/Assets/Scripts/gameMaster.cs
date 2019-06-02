using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameMaster : MonoBehaviour
{
    Player playerScript;
    playerMovement playerMovementScript;

    bool isLeaching = false;
    //this bool is used to make sure that leaching is not overlapped (called again before it is finished)

    private void Awake() {
        playerScript = FindObjectOfType<Player>();
        playerMovementScript = FindObjectOfType<playerMovement>();
    }

    private void Start() {
        if(playerScript == null) 
            Debug.LogError("Can't find object of type: player");

        if (playerMovementScript == null) 
            Debug.LogError("Can't find object of type: playerMovement");
    }


    public void Leach(int durration, int damageASec, float moveMultiply, int moveAdd, bool ispermanent, Transform projectile = null){
        if (!isLeaching) {
            StartCoroutine(playerScript.DamageLeach(2, durration));
            StartCoroutine(playerMovementScript.MovementLeach(moveMultiply, moveAdd, durration, ispermanent, projectile));
            StartCoroutine(isLeachControl(durration));
        } else {
            Destroy(projectile.gameObject);
            //make them have a small explosion or some effect
        }

        
        
    }

    IEnumerator isLeachControl(int durration) {
        isLeaching = true;
        yield return new WaitForSeconds(durration);
        isLeaching = false;
    }
}
