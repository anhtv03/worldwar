using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YugiAttackScript : MonoBehaviour {

    [SerializeField]
    GameObject spawn;

    [Header("List Monster")]
    [SerializeField]
    List<GameObject> monsterSummon;

    [SerializeField]
    List<GameObject> monsterSkills;

    [SerializeField]
    GameObject monsterUltimate;

    private Animator animator;
    private string currentKey;
    private Mana playerMana;
    private string _key;

    private void Awake() {
        playerMana = gameObject.GetComponent<Mana>();
    }

    void Start() {
        animator = gameObject.GetComponent<Animator>();
    }

    void Update() {
        var currentStateInfo = animator.GetCurrentAnimatorStateInfo(0);

        //----------------------tat tan cong--------------------------
        if (currentStateInfo.normalizedTime >= 1.0f) {
            if (currentStateInfo.IsName("yugi_attack")) {
                animator.SetBool(AnimationString.isAttack, false);
            } else if (currentStateInfo.IsName("yugi_skill")) {
                animator.SetBool(AnimationString.isSkill, false);
            } else if (currentStateInfo.IsName("yugi_ultimate")) {
                animator.SetBool(AnimationString.isUltimate, false);
            }
        }
    }

    //===================input system setting=========================
    public void OnAttack() {
        animator.SetBool(AnimationString.isAttack, true);
        setMovementOff();
    }

    public void OnSkill(string key) {
        if (playerMana.GetMana() > 20) {
            playerMana.UseMana(20);
            _key = key;
            animator.SetBool(AnimationString.isSkill, true);
            setMovementOff();
        }
    }

    public void OnUltimate() {
        if (playerMana.GetMana() == 100) {
            playerMana.UseMana(100);
            animator.SetBool(AnimationString.isUltimate, true);
            setMovementOff();
        }
    }


    //-------------------action of monster------------------------------
    public void SummonMonster() {
        GameObject mtSummon = Instantiate(
            RandomObject(monsterSummon),
            spawn.transform.position,
            Quaternion.identity
            );
        mtSummon.GetComponent<Collider2D>().enabled = false;
        MonsterDirection(mtSummon, mtSummon.GetComponent<MosterAttackScript>());

        setMovementOffAttack(mtSummon);
        if (mtSummon.gameObject.tag == "magic") {
            setManaByBullet(mtSummon);
        } else {
            setManaByPhysic(mtSummon);
        }
    }

    public void SummonMonsterSkill() {
        if (_key == "1") {
            SummonMonsterSkill1();
        } else {
            SummonMonsterSkill2();
        }
    }

    private void SummonMonsterSkill1() {
        GameObject monsterSkill = monsterSkills[0];
        GameObject mtSummon = Instantiate(monsterSkill, spawn.transform.position, Quaternion.identity);
        mtSummon.GetComponent<Collider2D>().enabled = false;
        MonsterDirection(mtSummon, mtSummon.GetComponent<MosterAttackScript>());

        setMovementOffAttack(mtSummon);
        setManaByBullet(mtSummon);
    }

    private void SummonMonsterSkill2() {
        GameObject monsterSkill = monsterSkills[1];
        GameObject mtSummon = Instantiate(monsterSkill, spawn.transform.position, Quaternion.identity);
        mtSummon.GetComponent<Collider2D>().enabled = false;
        MonsterDirection(mtSummon, mtSummon.GetComponent<MosterAttackScript>());

        setMovementOffAttack(mtSummon);
        setManaByPhysic(mtSummon);
    }

    public void SummonUltimateMonster() {
        GameObject mtSummon = Instantiate(monsterUltimate, spawn.transform.position, Quaternion.identity);
        mtSummon.GetComponent<Collider2D>().enabled = false;
        MonsterDirection(mtSummon, mtSummon.GetComponent<UltimateAttackScript>());

        setMovementOffUlti(mtSummon);
    }

    //=================logic in monster===========================
    private GameObject RandomObject(List<GameObject> moster) {
        int randomValue = Random.Range(0, moster.Count);
        return moster[randomValue];
    }

    private void MonsterDirection(GameObject mtSummon, Component monsterComponent) {
        Vector3 monsterScale = mtSummon.transform.localScale;
        float direction = transform.localScale.x < 0 ? -1 : 1;
        monsterScale.x = Mathf.Abs(monsterScale.x) * direction;
        mtSummon.transform.localScale = monsterScale;

        if (monsterComponent is MosterAttackScript monster) {
            monster.Speeds = Mathf.Abs(monster.Speeds) * direction;
        } else if (monsterComponent is UltimateAttackScript ultimateMonster) {
            ultimateMonster.Speeds = Mathf.Abs(ultimateMonster.Speeds) * direction;
        }
    }

    //---------set mana-----------------------------
    private void setManaByBullet(GameObject mtSummon) {
        MagicFireScript magicFireScript = mtSummon.GetComponent<MagicFireScript>();
        magicFireScript.SetOwnerMana(playerMana);
    }

    private void setManaByPhysic(GameObject mtSummon) {
        MosterAttackScript mosterAttackScript = mtSummon.GetComponent<MosterAttackScript>();
        mosterAttackScript.SetOwnerMana(playerMana);
    }

    //---------set movement-----------------------------
    private void setMovementOff() {
        GamePadController gamePadController = gameObject.GetComponent<GamePadController>();
        gamePadController.enabled = false;
    }

    private void setMovementOffAttack(GameObject mtSummon) {
        MosterAttackScript mosterAttackScript = mtSummon.GetComponent<MosterAttackScript>();
        mosterAttackScript.setCharacter(gameObject);
    }

    private void setMovementOffUlti(GameObject mtSummon) {
        UltimateAttackScript mosterAttackScript = mtSummon.GetComponent<UltimateAttackScript>();
        mosterAttackScript.setCharacter(gameObject);
    }

}
