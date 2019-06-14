using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class playerMovement : MonoBehaviour
{
    Rigidbody2D rb;

    [Header("Movement")]
    [Range(1, 30)]
    public float playerMovementSpeed = 5;
    Vector3 moveInput, joyMovementInput, joyRotationInput, moveVelocity, zeroVector = Vector3.zero;
    [SerializeField] int deviceAngle = 60;

    [SerializeField] Joystick joystickMov;
    [SerializeField] Joystick joystickRot;
    Quaternion lastKnownRot;

    [SerializeField] int rotationOffset = 45 + 90;

    [Range(0, 1)]
    [SerializeField] float movementSmoothing = .15f;

    [Header("Clamp")]
    [SerializeField] float clampWidth = 11;
    [SerializeField] float clampHeight = 6;

    
    
    void Start()
    {
        lastKnownRot = transform.rotation;
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
            Debug.LogError("Can't find a RigidBody for player movement");
    }


    void Update()
    {
        JoystickRotation();
        //RotateToMouse();
        //moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //moveVelocity = playerMovementSpeed * moveInput;

        //tilt = Quaternion.Euler(deviceAngle, 0, 0) * Input.acceleration * playerMovementSpeed;

        joyMovementInput = new Vector3(joystickMov.Horizontal, joystickMov.Vertical, 0) * playerMovementSpeed;
    }

    void FixedUpdate() {
        float clampedX = Mathf.Clamp(transform.position.x, -clampWidth, clampWidth);
        float clampedY = Mathf.Clamp(transform.position.y, -clampHeight, clampHeight);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, joyMovementInput, ref zeroVector, movementSmoothing);
        
    }

    void JoystickRotation() {
        
        if(joystickRot.Horizontal > 0 || joystickRot.Vertical > 0 || joystickRot.Horizontal < 0 || joystickRot.Vertical < 0) {
            Vector3 joyPos = new Vector3(joystickRot.Horizontal, joystickRot.Vertical, transform.position.z) * 5;
            Debug.DrawLine(transform.position, transform.position + joyPos);
            //transform.LookAt(transform.position + joyPos);

            Vector3 difference = (transform.position + joyPos) - transform.position;
            difference.Normalize();     // makes all sum of vector = to 1;

            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg; //finding the angle in degrees
            transform.rotation = Quaternion.Euler(0f, 0f, rotationZ + rotationOffset);
            lastKnownRot = transform.rotation;
        } else {
            transform.rotation = lastKnownRot;
        }
    }

    //void RotateToMouse() {
    //    Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    //    difference.Normalize();     // makes all sum of vector = to 1;

    //    float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg; //finding the angle in degrees
    //    transform.rotation = Quaternion.Euler(0f, 0f, rotationZ + rotationOffset);
    //}

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
