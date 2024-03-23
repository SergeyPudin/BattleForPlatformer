using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _currentHealth;
    [SerializeField] private int _maxHealthValue;

    private int _minHealthValue = 0;
    private bool _isDead = false;

    public void TakeDamage(int damage)
    {
        if (_isDead == false)
        {
            _currentHealth = Mathf.Clamp(_currentHealth - damage, _minHealthValue, _maxHealthValue);

            if (_currentHealth <= 0)            
                Die();
        }
    }

    public void Heal(int healthPoint)
    {
        if (_isDead == false)
            _currentHealth = Mathf.Clamp(_currentHealth + healthPoint, _minHealthValue, _maxHealthValue);
    }

    private void Die()
    {
        _isDead = true;

        Destroy(gameObject);
    }
}
