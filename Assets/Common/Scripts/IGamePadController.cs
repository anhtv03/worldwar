using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGamePadController
{
    void OnMove(Vector2 input);
    void OnJump();
    void OnDefend();
    void OnAttack();
    void OnDash();
    void OnSkill1();
    void OnSkill2();
    void OnCombo();

}
