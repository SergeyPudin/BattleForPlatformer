using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _health;

    private bool _isDead = false;

    public void TakeDamage(int damage)
    {
        int minValue = 0;
        int maxValue = _health;

        if (_isDead == false)
        {
            _health = Mathf.Clamp(_health - damage, minValue, maxValue);

            if (_health <= 0)            
                Die();
        }
    }

    public void Heal(int healthPoint)
    {
        if (_isDead == false)
            _health += healthPoint;
    }

    private void Die()
    {
        _isDead = true;

        Destroy(gameObject);
    }
}
