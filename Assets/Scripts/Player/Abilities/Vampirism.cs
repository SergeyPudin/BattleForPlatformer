using UnityEngine;

[RequireComponent(typeof(Health))]
public class Vampirism : MonoBehaviour
{
    [SerializeField] private float _vampirismRadius = 3.0f;
    [SerializeField] private float _vampirismTime = 6.0f;
    [SerializeField] private float _damagePerSecond = 1.0f / 6;
    [SerializeField] private float _healingCoefficient = 0.5f;
    [SerializeField] private CircleCollider2D _collider;

    private float _elapsedTime;

    private Health _health;
    private Enemy _enemy;

    private void Start()
    {
        _health = GetComponent<Health>();        
        _collider.radius = _vampirismRadius;
    }

    private void OnEnable()
    {
        _elapsedTime = _vampirismTime;
        _collider.enabled = true;
    }

    private void OnDisable()
    {
        _collider.enabled = false;
    }

    private void Update()
    {
        if (_elapsedTime > 0)
        {
            _elapsedTime -= Time.deltaTime;
        }
        else
        {
            enabled = false;
        }

        if (_enemy != null)
        {
            float damage = _damagePerSecond* Time.deltaTime;
            float healpoint = damage * _healingCoefficient;
            
            _enemy.TryGetComponent<Health>(out Health health);

            health.TakeDamage(damage);
            _health.Heal(healpoint);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            _enemy = enemy;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            _enemy = null;
        }
    }
}