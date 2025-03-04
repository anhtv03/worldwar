using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class PlayerController : MonoBehaviour, IGamePadController
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpInput = 8f;
    [SerializeField] float airMoveSpeed = 3f;
    [SerializeField] GameObject skill1Effect;
    [SerializeField] GameObject skill1EffectPosition;
    [SerializeField] GameObject skill2Image;
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
    AudioClipController audioClipController;
    Vector3 currentPosition;
    float skill2Time = 0;
    float comboTime = 0;
    bool isTeleporting = false;
    bool _isRunning = false;
    bool _isFacingRight = true;
    bool comboPowerOn = false;
    bool inCombo = false;
    int numOfTimeGetDamage = 0;
    public int NumberOfTimeGetDamage
    {
        get { return numOfTimeGetDamage; }
        set { numOfTimeGetDamage = value; }
    }

    public Vector2 MoveInput
    {
        get { return moveInput; }
        set { moveInput = value; }
    }
    int comboDashTurn = 0;
    public float CurrentMoveSpeed
    {
        get
        {
            if (IsMoving && !touchingDirections.IsOnWall)
            {
                if (touchingDirections.IsGrounded)
                {
                    return moveSpeed;
                }
                else
                {
                    return airMoveSpeed;
                }
            }
            return moveSpeed;
        }
        set
        {
            value = moveSpeed;
        }
    }


    public bool IsFacingRight
    {
        get
        {
            return _isFacingRight;
        }
        private set
        {
            if (_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }
    public bool IsMoving
    {
        get
        {
            return _isRunning;
        }
        private set
        {
            _isRunning = value;
            if (!combo)
            {
                animator.SetBool(AnimationString.isRunning, value);
            }
        }
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        playerMana = GetComponent<Mana>();
        hitBox.SetActive(false);
        touchingCol = GetComponent<CapsuleCollider2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        audioClipController = GetComponent<AudioClipController>();
    }

    public void HitBoxOn()
    {
        hitBox.SetActive(true);
    }
    public void HitBoxOff()
    {
        hitBox.SetActive(false);
    }

    void FixedUpdate()
    {
        if (isTeleporting)
        {
            skill2Time += Time.fixedDeltaTime;
            if (skill2Time >= 5)
            {
                isTeleporting = false;
                skill2Time = 0;
            }
        }
        if (combo)
        {
            comboTime += Time.fixedDeltaTime;
            if (comboTime >= 7)
            {
                inCombo = false;
                comboTime = 0;
                touchingCol.isTrigger = false;
            }
        }
        if (!combo)
        {
            rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
            animator.SetFloat(AnimationString.yVelocity, rb.velocity.y);
        }
    }



    public void OnMove(Vector2 input)
    {
        moveInput = input;
        if (inCombo && comboDashTurn <= 10)
        {
            if (transform.localScale.x >= 0)
            {
                audioClipController.PlaySlideClip();
                animator.SetTrigger(AnimationString.slideCombo);
                rb.position += new Vector2(2, 0);
                comboDashTurn++;
            }
            else
            {
                audioClipController.PlaySlideClip();
                animator.SetTrigger(AnimationString.slideCombo);
                rb.position -= new Vector2(2, 0);
                comboDashTurn++;
            }
        }
        else
        {
            IsMoving = moveInput != Vector2.zero;
            if (comboPowerOn)
            {   
                comboPower.SetActive(false);
            }
        }
        SetFacingDirection(moveInput);
    }



    void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }

    public void OnJump()
    {
        if (touchingDirections.IsGrounded)
        {
            animator.SetTrigger(AnimationString.jump);
            rb.velocity = new Vector2(rb.velocity.x, jumpInput);
        }
    }

    public void OnDash()
    {
        if (touchingDirections.IsGrounded)
        {
            animator.SetTrigger(AnimationString.dash);
            animator.SetBool(AnimationString.isDashing, true);
            audioClipController.FlashClip();
            if (transform.localScale.x >= 0)
            {
                rb.position += new Vector2(5, 0);
            }
            else
            {
                rb.position -= new Vector2(5, 0);
            }
        }
    }

    public void OnDefend()
    {
        if (touchingDirections.IsGrounded)
        {
            animator.SetTrigger(AnimationString.defend);
            animator.SetBool(AnimationString.isDefending, true);
        }
    }

    public void OnAttack()
    {
        comboPower.SetActive(false);
        comboPowerOn = false;
        int randomSkill = UnityEngine.Random.Range(0, 2);
        if (touchingDirections.IsGrounded)
        {
            if (randomSkill == 0)
            {
                audioClipController.PlayKickClip();
                animator.SetTrigger(AnimationString.kick);
                animator.SetBool(AnimationString.isKicking, true);

            }
            else
            {
                audioClipController.PlaySlideClip();
                animator.SetTrigger(AnimationString.groundSlash);
                animator.SetBool(AnimationString.isGroundSlashing, true);
            }
        }
        else if (!touchingDirections.IsGrounded)
        {
            if (randomSkill == 0)
            {
                audioClipController.PlayKickClip();
                animator.SetTrigger(AnimationString.kick);
                animator.SetBool(AnimationString.isKicking, true);

            }
            else
            {
                audioClipController.PlaySlideClip();
                animator.SetTrigger(AnimationString.airSlash);
                animator.SetBool(AnimationString.isAirSlashing, true);
            }
        }
    }

    public void OnSkill1()
    {
        comboPower.SetActive(false);
        comboPowerOn = false;
        if (playerMana.GetMana() >= 20)
        {
            if (touchingDirections.IsGrounded)
            {
                audioClipController.PlaySlideClip();
                animator.SetTrigger(AnimationString.skill1);
                animator.SetBool(AnimationString.isSkill1, true);
            }
            else if (!touchingDirections.IsGrounded)
            {
                audioClipController.PlaySlideClip();
                animator.SetTrigger(AnimationString.skill1);
            }
            playerMana.UseMana(20);
        }
        else
        {
            // Chen audio
            return;
        }
    }

    void Effect1()
    {
        skill1Effect.transform.position = new Vector2(skill1EffectPosition.transform.position.x, skill1EffectPosition.transform.position.y);

        if (touchingDirections.IsGrounded)
        {
            playerMana.UseMana(20);
            if (transform.localScale.x > 0)
            {
                Instantiate(skill1Effect, skill1Effect.transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(skill1Effect, skill1Effect.transform.position, Quaternion.Euler(0, 180, 0));
            }
        }

    }

    public void OnSkill2()
    {
        comboPower.SetActive(false);
        comboPowerOn = false;
        if (playerMana.GetMana() >= 20)
        {
            if (touchingDirections.IsGrounded)
            {
                animator.SetTrigger(AnimationString.skill2);
                animator.SetBool(AnimationString.isSkill2, true);
            }
            else if (!touchingDirections.IsGrounded)
            {
                animator.SetTrigger(AnimationString.skill2);
                animator.SetBool(AnimationString.isSkill2, true);
            }
            playerMana.UseMana(20);
        }
        else
        {
            // Chen audio het mana

            return;
        }

    }

    public void ComboSlide()
    {
        float x = skill1EffectPosition.transform.position.x;
        if(transform.localScale.x > 0)
        {
            x += 1;
        } else
        {
            x -= 1;
        }
        skill1Effect.transform.position = new Vector2(x, skill1EffectPosition.transform.position.y + 0.5f);
        if (transform.localScale.x > 0)
        {

            Instantiate(comboSlide, skill1Effect.transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(comboSlide, skill1Effect.transform.position, Quaternion.Euler(0, 180, 0));
        }
    }
    void Skill2Image()
    {
        Vector3 leftEdgePosition = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.5f, Camera.main.nearClipPlane));
        leftEdgePosition.x -= skill2Image.GetComponentInChildren<SpriteRenderer>().bounds.size.x / 2;
        Instantiate(skill2Image, new Vector3(-0.209999993f, -0.660000026f, -9.70001221f), Quaternion.identity);
    }

    public void Skill2Teleport()
    {

        if (!isTeleporting)
        {
            currentPosition = transform.position;
            comboPower.SetActive(false);
            comboPowerOn = false;

            skill2Time = 0;
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Player");
            StartCoroutine(Tele());
            playerMana.UseMana(20);
            GameObject otherPlayer;
            foreach (var item in enemies)
            {
                if (item != gameObject)
                {
                    otherPlayer = item;
                    transform.position = otherPlayer.transform.position;
                    break;
                }
            }
            
            
            isTeleporting = true;

            audioClipController.TeleportClip();
        }
        else
        {
            comboPower.SetActive(false);
            comboPowerOn = false;
            StartCoroutine(Tele());
            transform.position = currentPosition;
            isTeleporting = false;
            audioClipController.TeleportClip();

        }


    }

    public void OnCombo()
    {

        if (playerMana.GetMana() == 100)
        {
            if (touchingDirections.IsGrounded)
            {
                audioClipController.ComboClip();
                videoUlti.SetActive(true);
                GamePadController playerInput = GetComponent<GamePadController>();
                playerInput.enabled = false;
                StartCoroutine(ComboPower());
                playerMana.UseMana(100);
                animator.SetTrigger(AnimationString.combo);
                comboPowerOn = true;
                inCombo = true;
                comboDashTurn = 0;
                touchingCol.isTrigger = true;
            }
        }
        else
        {
            // Chen audio het mana
            return;
        }
        
    }

    public void TurnOnControll()
    {
        GamePadController playerInput = GetComponent<GamePadController>();
        playerInput.enabled = true;
    }

    IEnumerator ComboPower()
    {
        yield return new WaitForSeconds(2.35f);
        TurnOnControll();
        videoUlti.SetActive(false);
    }

    public void ShowPowerCombo()
    {
        comboPower.SetActive(true);
    }

    IEnumerator Tele()
    {
        yield return new WaitForSeconds(1);
    }
}
