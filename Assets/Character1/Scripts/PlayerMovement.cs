using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {
    [SerializeField]
    float moveSpeed = 3f;

    [SerializeField]
    float jumpSpeed = 8f;

    Vector2 moveInput;
    bool isJumping = false;
    Rigidbody2D playerRigidbody;
    Animator playerAnimator;

    void Start() {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {


        Run();
        Flip();
        if (IsMoving()) {
            transform.localScale = new Vector2(Mathf.Sign(playerRigidbody.velocity.x), 1f);
        }
        SetIdle();

    }

    void OnMove(InputValue value) {
        if (!playerRigidbody.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }

        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value) {
        if (!playerRigidbody.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }

        if (value.isPressed) {
            // do stuff
            Jump();
            isJumping = true;


        }
    }

    void SetIdle() {
        if (isJumping && IsTouchGround()) {
            playerAnimator.SetBool("isJumpingWhenIdling", false);
            isJumping = false;
        }
    }

    bool IsTouchGround() {
        return playerRigidbody.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }

    void Jump() {
        Vector2 velocity = new Vector2(playerRigidbody.velocity.x, jumpSpeed);
        playerRigidbody.velocity = velocity;
        playerAnimator.SetBool("isJumpingWhenIdling", IsJumping());
    }

    bool IsMoving() {
        return Mathf.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon;
    }

    bool IsJumping() {
        return Mathf.Abs(playerRigidbody.velocity.y) > Mathf.Epsilon;
    }

    void Run() {
        Vector2 velocity = new Vector2(moveInput.x * moveSpeed, playerRigidbody.velocity.y);
        playerRigidbody.velocity = velocity;
        bool playerHasHorizontalSpeed = Mathf.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon;

        playerAnimator.SetBool("isRunning", IsMoving());
    }

    void Flip() {
        transform.localScale = new Vector2(Mathf.Sign(playerRigidbody.velocity.x), 1f);
    }
}
