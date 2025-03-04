using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour
{
    private int _mana = 0;

    public UnityEngine.UI.Image ManaBar { get; set; }

    public int Mp
    {
        get
        {
            return _mana;
        }
        set
        {
            _mana = value;
        }
    }
    public void UseMana(int amount)
    {
        // Ve lai UI
        _mana -= amount;
        _mana = (_mana <= 0) ? 0 : _mana;
        ManaBar.fillAmount = (float)_mana / 100;
    }

    public int GetMana()
    {
        return _mana;
    }

    public void GetMana(int manaBonus)
    {
        _mana += manaBonus;
        _mana = (_mana >= 100) ? 100 : _mana;

        // Ve lai UI
        ManaBar.fillAmount = (float)_mana / 100;
    }
}
