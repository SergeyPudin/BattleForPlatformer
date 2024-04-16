using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IChangeValue
{
    [SerializeField] private int _currentHealth;
    [SerializeField] private int _maxHealthValue;

    private int _minHealthValue = 0;
    private bool _isDead = false;

    public event UnityAction<int, int> OnValueChanged;
    public event UnityAction<int, int> Reset;
    public event UnityAction Dyed;

    private void Start()
    {
        Reset?.Invoke(_currentHealth, _maxHealthValue);
    }

    public void TakeDamage(int damage)
    {
        if (_isDead == false)
        {
            _currentHealth = Mathf.Clamp(_currentHealth - damage, _minHealthValue, _maxHealthValue);
            OnValueChanged?.Invoke(_currentHealth, _maxHealthValue);

            if (_currentHealth <= 0)            
                Die();
        }
    }

    public void Die()
    {
        _isDead = true;

       Dyed?.Invoke();
    }

    public void Heal(int healthPoint)
    {
        if (_isDead == false)
        {
            _currentHealth = Mathf.Clamp(_currentHealth + healthPoint, _minHealthValue, _maxHealthValue);
            OnValueChanged?.Invoke(_currentHealth, _maxHealthValue);
        }
    }
}
