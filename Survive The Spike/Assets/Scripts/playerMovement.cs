using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class playerMovement : MonoBehaviour
{
    Rigidbody2D rb;

    [Header("Movement")]
    [Range(1, 30)]
    [SerializeField] float playerMovementSpeed = 5;
    Vector2 moveInput, moveVelocity, zeroVector = Vector2.zero;

    [Range(0, 1)]
    [SerializeField] float movementSmoothing = .15f;

    [Header("Clamp")]
    [SerializeField] float clampWidth = 11;
    [SerializeField] float clampHeight = 6;

    
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
            Debug.LogError("Can't find a RigidBody for player movement");
    }


    void Update()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = playerMovementSpeed * moveInput;
    }

    void FixedUpdate() {
        float clampedX = Mathf.Clamp(transform.position.x, -clampWidth, clampWidth);
        float clampedY = Mathf.Clamp(transform.position.y, -clampHeight, clampHeight);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        rb.velocity = Vector2.SmoothDamp(rb.velocity, moveVelocity, ref zeroVector, movementSmoothing);
        
    }

    public IEnumerator MovementLeach(float multiply, float add, int effectDurration, bool ispermanent, Transform _projectile = null) {
        if (ispermanent) {
            playerMovementSpeed *= multiply;
            playerMovementSpeed += add;
            //permanently alters the speed
            yield return false;
        }
        else {
            float ogMovementSpeed = playerMovementSpeed;
            //saves the original speed so we could get it back
            float targetMovementSpeed = (playerMovementSpeed * multiply) + add;
            //creates a temp target speed

            playerMovementSpeed = targetMovementSpeed;
            //changed the player speed

            yield return new WaitForSeconds(effectDurration);
            //waits X seconds

            //Destroy(_projectile.gameObject);
            //Debug.Log(_projectile.gameObject.name + " Destroyed because leaching has ended");
            playerMovementSpeed = ogMovementSpeed;
            //returns movement speed to the original speed
        }  
    }
}
