using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicFireScript : MonoBehaviour {

    [SerializeField]
    GameObject FireBall;

    [SerializeField]
    GameObject spawn;

    private Mana ownerMana;

    public void Fire() {
        GameObject mtSummon = Instantiate(FireBall, spawn.transform.position, Quaternion.identity);
        MonsterDirection(mtSummon);
        BulletMovement bulletMovement = mtSummon.GetComponent<BulletMovement>();
        bulletMovement.SetOwnerMana(ownerMana);
    }

    private void MonsterDirection(GameObject mtSummon) {
        BulletMovement _moster = mtSummon.GetComponent<BulletMovement>();

        Vector3 monsterScale = mtSummon.transform.localScale;
        float direction = transform.localScale.x < 0 ? -1 : 1;

        _moster.Speeds = Mathf.Abs(_moster.Speeds) * direction;
    }

    public void SetOwnerMana(Mana mana) {
        ownerMana = mana;
    }
}
