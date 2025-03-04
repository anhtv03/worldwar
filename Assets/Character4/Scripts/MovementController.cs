using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class MovementController : MonoBehaviour, IGamePadController {

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpInput = 8f;
    [SerializeField] float airMoveSpeed = 3f;
    [SerializeField] bool combo = false;
    [SerializeField] GameObject comboPower;
    [SerializeField] GameObject comboSlide;
    [SerializeField] GameObject hitBox;
    [SerializeField] GameObject videoUlti;
    CapsuleCollider2D touchingCol;
    BoxCollider2D boxCollider;
    Mana playerMana;
    Vector2 moveInput;
    Rigidbody2D rb;
    Animator animator;
    TouchingDirections touchingDirections;
    Vector3 currentPosition;
    float skill2Time = 0;
    float comboTime = 0;
    bool isTeleporting = false;
    bool _isRunning = false;
    bool _isFacingRight = true;
    bool comboPowerOn = false;
    bool inCombo = false;
    YugiAttackScript YugiAttackScript;
    int numOfTimeGetDamage = 0;

    public int NumberOfTimeGetDamage {
        get { return numOfTimeGetDamage; }
        set { numOfTimeGetDamage = value; }
    }

    public Vector2 MoveInput {
        get { return moveInput; }
        set { moveInput = value; }
    }
    int comboDashTurn = 0;
    public float CurrentMoveSpeed {
        get {
            if (IsMoving && !touchingDirections.IsOnWall) {
                if (touchingDirections.IsGrounded) {
                    return moveSpeed;
                } else {
                    return airMoveSpeed;
                }
            }
            return moveSpeed;
        }
        set {
            value = moveSpeed;
        }
    }


    public bool IsFacingRight {
        get {
            return _isFacingRight;
        }
        private set {
            if (_isFacingRight != value) {
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }
    public bool IsMoving {
        get {
            return _isRunning;
        }
        private set {
            _isRunning = value;
            if (!combo) {
                animator.SetBool(AnimationString.isRunning, value);
            }
        }
    }

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        playerMana = GetComponent<Mana>();
        YugiAttackScript = GetComponent<YugiAttackScript>();
        touchingCol = GetComponent<CapsuleCollider2D>();
    }

    void FixedUpdate() {
        if (isTeleporting) {
            skill2Time += Time.fixedDeltaTime;
            if (skill2Time >= 5) {
                isTeleporting = false;
                skill2Time = 0;
            }
        }
        if (combo) {
            comboTime += Time.fixedDeltaTime;
            if (comboTime >= 7) {
                inCombo = false;
                comboTime = 0;
                touchingCol.isTrigger = false;
            }
        }
        if (!combo) {
            rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
            animator.SetFloat(AnimationString.yVelocity, rb.velocity.y);
        }
    }



    public void OnMove(Vector2 input) {
        moveInput = input;
        if (inCombo && comboDashTurn <= 10) {
            if (transform.localScale.x >= 0) {
                animator.SetTrigger(AnimationString.slideCombo);
                rb.position += new Vector2(2, 0);
                comboDashTurn++;
            } else {
                animator.SetTrigger(AnimationString.slideCombo);
                rb.position -= new Vector2(2, 0);
                comboDashTurn++;
            }
        } else {
            IsMoving = moveInput != Vector2.zero;
            if (comboPowerOn) {
                comboPower.SetActive(false);
            }
        }
        SetFacingDirection(moveInput);
    }


    void SetFacingDirection(Vector2 moveInput) {
        if (moveInput.x > 0 && !IsFacingRight) {
            IsFacingRight = true;
        } else if (moveInput.x < 0 && IsFacingRight) {
            IsFacingRight = false;
        }
    }

    public void OnJump() {
        if (touchingDirections.IsGrounded) {
            animator.SetTrigger(AnimationString.jump);
            rb.velocity = new Vector2(rb.velocity.x, jumpInput);
        }
    }

    public void OnDash() {
        if (touchingDirections.IsGrounded) {
            animator.SetTrigger(AnimationString.dash);
            animator.SetBool(AnimationString.isDashing, true);
            if (transform.localScale.x >= 0) {
                rb.position += new Vector2(5, 0);
            } else {
                rb.position -= new Vector2(5, 0);
            }
        }
    }

    public void OnDefend() {
        if (touchingDirections.IsGrounded) {
            animator.SetTrigger(AnimationString.defend);
            animator.SetBool(AnimationString.isDefending, true);
        }
    }

    public void OnAttack() {
        YugiAttackScript.OnAttack();
    }

    public void OnSkill1() {
        YugiAttackScript.OnSkill("1");
    }

    public void OnSkill2() {
        YugiAttackScript.OnSkill("2");
    }

    public void OnCombo() {
        YugiAttackScript.OnUltimate();
    }
}
