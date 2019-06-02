using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    Transform player;
    SpriteRenderer SR;
    playerMovement pMovement;

    bool latched = false;
    bool hitAnother = false;
    Vector3 latchOffset;

    void Start()
    {
        pMovement = FindObjectOfType<playerMovement>();
        SR = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (player == null)
            Debug.LogError("Can't find player transformreference");
        if (SR == null)
            Debug.LogError("Can't find sprite renderer reference");
        if (pMovement == null)
            Debug.LogError("Can't find playerMovemt reference");

        
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.collider.tag == "Projectile" && !hitAnother) {
            hitAnother = true;
            StartCoroutine(fadeOut(gameObject.transform));
            return;
        // if a projectile hits another projectile that is on the player then it will be destroyed
        }
        if (!latched) {
            if (collision.collider.tag == "Player") {
                //Debug.Log("hit player");
                player = collision.collider.transform;
                gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                Destroy(gameObject.GetComponent<Rigidbody2D>());
                transform.SetParent(player.parent);

                //starts the LEACHING
                //FindObjectOfType<gameMaster>().Leach(5, 2, 0.1f, 0, false, transform);
                FindObjectOfType<Shooting>().callLeach();

                hitAnother = true;
                latched = true;
                return;
            }
        } 
    }
    public IEnumerator fadeOut(Transform _object) {
        SR = GetComponent<SpriteRenderer>();
        if (SR == null)
            Debug.LogError("Can't find sprite renderer reference");

        Color newColor = SR.color;
        newColor.a = SR.color.a;
        Debug.Log("Inside of fade");
        for(float i = newColor.a; i > 0; i -= .1f) {
            Debug.Log("INSIDE FADE LOOP: " + i);
            newColor.a = i;
            SR.color = newColor;
            yield return new WaitForSeconds(.1f);
        }
        Debug.Log("After Fade loop");

        if (_object == null)
            Debug.LogError("no object to destroy");
        Destroy(_object.gameObject);
        

    }
}

