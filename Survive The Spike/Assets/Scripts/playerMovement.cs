using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    bool isLeaching = false;
    float playerMovementSpeed = 5;
    Rigidbody2D rb;

    [Header("Movement")]
    [Range(1, 30)]
    
    Vector2 moveInput, moveVelocity, zeroVector = Vector2.zero;

    [Range(0, 1)]
    [SerializeField] float movementSmoothing = .15f;

    [Header("Clamp")]
    [SerializeField] float clampWidth = 11;
    [SerializeField] float clampHeight = 6;

    
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
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

        if (!isLeaching) {
            if (ispermanent) {
                playerMovementSpeed *= multiply;
                playerMovementSpeed += add;
                yield return false;
            }
            else {
                float ogMovementSpeed = playerMovementSpeed;
                float targetMovementSpeed = (playerMovementSpeed * multiply) + add;

                playerMovementSpeed = targetMovementSpeed;

                isLeaching = true;
                Debug.Log("Leaching");
                yield return new WaitForSeconds(effectDurration);
                isLeaching = false;
                Destroy(_projectile.gameObject);
                Debug.Log("UNleaching");
                playerMovementSpeed = ogMovementSpeed;
            }
        }
        else {
            Destroy(_projectile.gameObject);
            yield return false;
        }

        
        
    }
}
