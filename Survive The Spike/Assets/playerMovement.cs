using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    Rigidbody2D rb;

    [Header("Movement")]
    [Range(1, 30)]
    [SerializeField] float movementSpeed = 15f;
    Vector2 moveInput, moveVelocity, zeroVector = Vector2.zero;

    [Range(0, 1)]
    [SerializeField] float movementSmoothing = .15f;

    [Header("Clamp")]
    [SerializeField] float clampWidth = 11;
    [SerializeField] float clampHeight = 6;

    bool once = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }


    void Update()
    {
        
        if (!once) {
            StartCoroutine(alterMoveSpeed(1, 200,5, false));
            once = true;
        }
        Debug.Log(movementSpeed);

        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = movementSpeed * moveInput;
    }

    void FixedUpdate() {
        float clampedX = Mathf.Clamp(transform.position.x, -clampWidth, clampWidth);
        float clampedY = Mathf.Clamp(transform.position.y, -clampHeight, clampHeight);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        rb.velocity = Vector2.SmoothDamp(rb.velocity, moveVelocity, ref zeroVector, movementSmoothing);
    }

    public IEnumerator alterMoveSpeed(float multiply, float add, float effectDurration, bool ispermanent) {

        if (ispermanent) {
            movementSpeed *= multiply;
            movementSpeed += add;
            yield return false;
        }
        else {
            float ogMovementSpeed = movementSpeed;
            float targetMovementSpeed = (movementSpeed * multiply) + add;

            movementSpeed = targetMovementSpeed;
            yield return new WaitForSeconds(effectDurration);
            movementSpeed = ogMovementSpeed;
            
        }
        
    }
}
