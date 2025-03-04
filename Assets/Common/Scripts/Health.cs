using UnityEngine;

public class Health : MonoBehaviour
{
    private int _health = 1000;

    public UnityEngine.UI.Image HealthBar { get; set; }

    public int Hp
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
        }
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        // Ve lai UI
        HealthBar.fillAmount = (float)_health / 1000;

    }

    public void GetHealth(int hp)
    {
        _health += hp;
        _health = (_health >= 1000) ? 1000 : _health;

        // Ve lai UI
        HealthBar.fillAmount = (float)_health / 1000;
    }

}
