using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameMaster : MonoBehaviour
{
    Player playerScript;
    playerMovement playerMovementScript;
    projectile projectileScript;

    bool isLeaching = false;
    //this bool is used to make sure that leaching is not overlapped (called again before it is finished)

    private void Awake() {
        playerScript         = FindObjectOfType<Player>();
        playerMovementScript = FindObjectOfType<playerMovement>();
        projectileScript     = FindObjectOfType<projectile>();
    }

    private void Start() {
        if(playerScript == null) 
            Debug.LogError("Can't find object of type: player");

        if (playerMovementScript == null) 
            Debug.LogError("Can't find object of type: playerMovement");

        if (projectileScript == null) 
            Debug.LogError("Can't find object of type: projectile");
    }


    public void Leach(int durration, int damageASec, float moveMultiply, int moveAdd, bool ispermanent, Transform projectile = null){
        if (!isLeaching) {
            StartCoroutine(playerScript.DamageLeach(2, durration));
            StartCoroutine(playerMovementScript.MovementLeach(moveMultiply, moveAdd, durration, ispermanent, projectile));
            StartCoroutine(isLeachAndFadeControl(durration, projectile));
        } else {
            Debug.Log("!LEACHING");
            StartCoroutine(projectileScript.fadeOut(projectile));
        }

        
        
    }

    IEnumerator isLeachAndFadeControl(int durration, Transform _projectile) {
        isLeaching = true;
        yield return new WaitForSeconds(durration);
        isLeaching = false;
        StartCoroutine(projectileScript.fadeOut(_projectile));
    }
}
