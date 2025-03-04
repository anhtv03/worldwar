using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private IGamePadController playerController;
    [SerializeField]
    private int playerIndex;

    public int PlayerIndex
    {
        get
        {
            return playerIndex;
        }
        set
        {
            playerIndex = value;
        }
    }
    void Start()
    {
        playerController = GetComponent<IGamePadController>();
    }

    void Update()
    {
        InputPlayController();
    }

    private void InputPlayController()
    {
        if (playerIndex == 1)
        {
            if (Input.GetKeyDown(KeyCode.A)) playerController.OnMove(Vector2.left);
            if (Input.GetKeyUp(KeyCode.A)) playerController.OnMove(Vector2.zero);
            if (Input.GetKeyDown(KeyCode.D)) playerController.OnMove(Vector2.right);
            if (Input.GetKeyUp(KeyCode.D)) playerController.OnMove(Vector2.zero);
            if (Input.GetKeyDown(KeyCode.W)) playerController.OnJump();
            if (Input.GetKeyDown(KeyCode.S)) playerController.OnDefend();
            if (Input.GetKeyDown(KeyCode.J)) playerController.OnAttack();
            if (Input.GetKeyDown(KeyCode.L)) playerController.OnDash();
            if (Input.GetKeyDown(KeyCode.U)) playerController.OnSkill1();
            if (Input.GetKeyDown(KeyCode.I)) playerController.OnSkill2();
            if (Input.GetKeyDown(KeyCode.O)) playerController.OnCombo();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) playerController.OnMove(Vector2.left);
            if (Input.GetKeyUp(KeyCode.LeftArrow)) playerController.OnMove(Vector2.zero);
            if (Input.GetKeyDown(KeyCode.RightArrow)) playerController.OnMove(Vector2.right);
            if (Input.GetKeyUp(KeyCode.RightArrow)) playerController.OnMove(Vector2.zero);
            if (Input.GetKeyDown(KeyCode.UpArrow)) playerController.OnJump();
            if (Input.GetKeyDown(KeyCode.DownArrow)) playerController.OnDefend();
            if (Input.GetKeyDown(KeyCode.Keypad1)) playerController.OnAttack();
            if (Input.GetKeyDown(KeyCode.Keypad3)) playerController.OnDash();
            if (Input.GetKeyDown(KeyCode.Keypad4)) playerController.OnSkill1();
            if (Input.GetKeyDown(KeyCode.Keypad5)) playerController.OnSkill2();
            if (Input.GetKeyDown(KeyCode.Keypad6)) playerController.OnCombo();
        }
    }
}
