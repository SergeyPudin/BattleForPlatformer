using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IChangeValue
{
    [SerializeField] private float _currentHealth;
    [SerializeField] private float _maxHealthValue;

    private float _minHealthValue = 0;
    private bool _isDead = false;

    public event UnityAction<float, float> OnValueChanged;
    public event UnityAction<float, float> Reset;
    public event UnityAction Dyed;

    private void Start()
    {
        Reset?.Invoke(_currentHealth, _maxHealthValue);
    }

    public void TakeDamage(float damage)
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

    public void Heal(float healthPoint)
    {
        if (_isDead == false)
        {
            _currentHealth = Mathf.Clamp(_currentHealth + healthPoint, _minHealthValue, _maxHealthValue);
            OnValueChanged?.Invoke(_currentHealth, _maxHealthValue);
        }
    }
}