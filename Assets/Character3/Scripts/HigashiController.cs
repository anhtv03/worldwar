using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Character3.Scripts
{
  
    public class HigashiController : MonoBehaviour, IGamePadController
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float jumpInput = 8f;
        [SerializeField] private float airMoveSpeed = 3f;
        Vector2 moveInput;
        Rigidbody2D rb;
        Animator animator;
        TouchingDirections touchingDirections;
        HigashiSkill higashiSkill;
        HigashiAttack higashiAttack;

        bool _isRunning = false;
        bool _isFacingRight = true;

       

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
                animator.SetBool("isRunning", value);
            }
        }

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            touchingDirections = GetComponent<TouchingDirections>();
            higashiSkill = GetComponent<HigashiSkill>();
            higashiAttack = GetComponent<HigashiAttack>();
        }

        void FixedUpdate()
        {
            rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
            animator.SetFloat(AnimationString.yVelocity, rb.velocity.y);
        }

        public void OnMove(Vector2 input)
        {
            moveInput = input;
            IsMoving = moveInput != Vector2.zero;
            SetFacingDirection(moveInput);

        }

        public void SetFacingDirection(Vector2 moveInput)
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
                animator.SetBool("jump", true);
                rb.velocity = new Vector2(rb.velocity.x, jumpInput);
            }
        }


        public void OnDefend()
        {
            throw new System.NotImplementedException();
        }

        public void OnAttack()
        {
            higashiAttack.OnAttack();
        }

        public void OnDash()
        {
            throw new System.NotImplementedException();
        }

        public void OnSkill1()
        {
            higashiSkill.OnSkillTornado();
        }

        public void OnSkill2()
        {
            higashiSkill.OnSkill2();
        }

        public void OnCombo()
        {
            higashiSkill.OnSkillUltimate();
        }
    }
}