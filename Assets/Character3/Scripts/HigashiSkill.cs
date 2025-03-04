using Assets.Character3.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HigashiSkill : MonoBehaviour
{

    [SerializeField] GameObject TornadoPrefab;
    [SerializeField] Transform TornadoSpawnPoint;
    [SerializeField] GameObject Skill2Prefab;
    [SerializeField] Transform Skill2SpawnPoint;
    [SerializeField] GameObject UltimatePrefab;
    [SerializeField] Transform UltimateSpawnPoint;
    [SerializeField] float TornadoSpeed ; 

    // String Trigger 
    internal static string SkillTornado = "SkillTonardo";
    internal static string Skill2 = "Skill2";
    internal static string SkillUltimate = "UltimateSkill";
    Mana mana;


    private Animator animator;
    HigashiController higashiController;
   
    private bool canCastSkill = true;

    private void Awake()
    {
        animator = GetComponent<Animator>(); 
        higashiController = GetComponent<HigashiController>();
        mana = GetComponent<Mana>();
    }



    public void OnSkillTornado()
    {
        if (canCastSkill && mana.GetMana() > 0)
        {
            animator.SetTrigger(SkillTornado);
            mana.UseMana(20);
        }
    }

    public IEnumerator TonardoObjectSpawn()
    {

        canCastSkill = false;
        GameObject tonardo = Instantiate(TornadoPrefab, TornadoSpawnPoint.position, Quaternion.identity);
        if (!higashiController.IsFacingRight)
        {

            Vector3 localScale = tonardo.transform.localScale;
            localScale.x *= -1; 
            tonardo.transform.localScale = localScale;
        }

        Rigidbody2D rbReb = tonardo.GetComponent<Rigidbody2D>();
        if (rbReb != null)
        {
            
            rbReb.velocity = new Vector2((higashiController.IsFacingRight ? 1 : -1) * TornadoSpeed, 0f); 
        }
        TornadoScripts TonardoObject = tonardo.GetComponent<TornadoScripts>();
        if (TonardoObject != null)
        {
            TonardoObject.damageAmount = 100;
            yield return new WaitForSeconds(TonardoObject.LifeTime());  
        }

    
        canCastSkill = true;
    }

    public void OnSkill2()
    {
        if (canCastSkill && mana.GetMana() > 0)
        {
            animator.SetTrigger(Skill2);
            mana.UseMana(20);
        }
    }


    public IEnumerator Skill2Spawn()
    {

        canCastSkill = false;
        GameObject skill2 = Instantiate(Skill2Prefab, Skill2SpawnPoint.position, Quaternion.identity);
        if (!higashiController.IsFacingRight)
        {
            Vector3 localScale = skill2.transform.localScale;
            localScale.x *= -1; 
            skill2.transform.localScale = localScale;
        }

        Rigidbody2D rbReb = skill2.GetComponent<Rigidbody2D>();
        if (rbReb != null)
        {
            rbReb.velocity = new Vector2((higashiController.IsFacingRight ? 1 : -1) * TornadoSpeed, 0f);
        }
        TornadoScripts TonardoObject = skill2.GetComponent<TornadoScripts>();
        if (TonardoObject != null)
        {
            TonardoObject.damageAmount = 100;
            yield return new WaitForSeconds(TonardoObject.LifeTime());
            
        }
        canCastSkill = true;
    }


    public void OnSkillUltimate()
    {
        if ( canCastSkill && mana.GetMana() >= 30)
        {
            animator.SetTrigger(SkillUltimate);
            mana.UseMana(100);
        }
    }


    public IEnumerator SkillUltimateSSpawn()
    {

        canCastSkill = false;
        GameObject ultimate = Instantiate(UltimatePrefab, UltimateSpawnPoint.position, Quaternion.identity);
        if (!higashiController.IsFacingRight)
        {
            Vector3 localScale = ultimate.transform.localScale;
            localScale.x *= -1; 
            ultimate.transform.localScale = localScale;
        }
        Rigidbody2D rbReb = ultimate.GetComponent<Rigidbody2D>();
        if (rbReb != null)
        {
            rbReb.velocity = new Vector2((higashiController.IsFacingRight ? 1 : -1) * TornadoSpeed, 0f);
        }
        TornadoScripts TonardoObject = ultimate.GetComponent<TornadoScripts>();
        if (TonardoObject != null)
        {
            TonardoObject.damageAmount = 200;
            yield return new WaitForSeconds(TonardoObject.LifeTime());
        }

        canCastSkill = true;
    }


}
