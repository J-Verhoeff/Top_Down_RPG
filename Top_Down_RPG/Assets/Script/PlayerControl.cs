using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5.0f;
    [SerializeField] private float moveLimiter = 0.5f;
    
    private Animator controller;
    private Rigidbody2D rb;
    private bool isFacingRight = false;
    private bool isFacingLeft = false;
    private bool isFacingUp = false;
    private float movementX;
    private float movementY;

    private void Awake() {
        controller = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        movementX = Input.GetAxis("Horizontal") * movementSpeed;
        movementY = Input.GetAxis("Vertical") * movementSpeed;

        // set the speed
        controller.SetFloat("Speed", Mathf.Abs(Mathf.Sqrt(Mathf.Pow(movementX, 2) + Mathf.Pow(movementY, 2))));

        // set the direction
        if(movementY > 0) { // up
            isFacingRight = false;
            isFacingLeft = false;
            isFacingUp = true;
        }
        if (movementY < 0) { // down
            isFacingRight = false;
            isFacingLeft = false;
            isFacingUp = false;
        }
        if (movementX < 0) { // left
            isFacingRight = false;
            isFacingLeft = true;
            isFacingUp = false;
        }
        if (movementX > 0) { // right
            isFacingRight = true;
            isFacingLeft = false;
            isFacingUp = false;
        }
        controller.SetBool("IsLeft", isFacingLeft);
        controller.SetBool("IsRight", isFacingRight);
        controller.SetBool("IsUp", isFacingUp);

        // Fire weapon
        if(Input.GetButtonDown("Fire")) {
            controller.SetTrigger("Fire");
        }
    }

    private void FixedUpdate() {
        if (movementX != 0 && movementY != 0) { // limit diagonal movement
            movementX *= moveLimiter;
            movementY *= moveLimiter;
        }
        rb.velocity = new Vector2(movementX, movementY);
    }
}
