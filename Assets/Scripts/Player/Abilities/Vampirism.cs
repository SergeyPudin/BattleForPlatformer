using UnityEngine;

[RequireComponent(typeof(Health))]
public class Vampirism : MonoBehaviour
{
    [SerializeField] private float _vampirismRadius = 3.0f;
    [SerializeField] private float _vampirismTime = 6.0f;
    [SerializeField] private float _damagePerSecond = 1.0f / 6;
    [SerializeField] private float _healingCoefficient = 0.5f;
    [SerializeField] private CircleCollider2D _collider;
    [SerializeField] private Transform _circle;

    private float _elapsedTime;

    private Health _playersHealth;
    private Health _enemysHealth;
    private Enemy _enemy;

    private void Awake()
    {
        float diametr = _vampirismRadius * 2;

        _playersHealth = GetComponent<Health>();
        _collider.radius = _vampirismRadius;
        _circle.localScale = Vector3.one * diametr;
        _circle.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _elapsedTime = _vampirismTime;
        _collider.enabled = true;
        _circle.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        _collider.enabled = false;
        _circle.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (_elapsedTime > 0)
        {
            _elapsedTime -= Time.fixedDeltaTime;
        }
        else
        {
            enabled = false;
        }

        if (_enemy != null)
        {
            float damage = _damagePerSecond * Time.fixedDeltaTime;
            float healpoint = damage * _healingCoefficient;

            _enemysHealth.TakeDamage(damage);
            _playersHealth.Heal(healpoint);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out Enemy enemy) && enemy.TryGetComponent<Health>(out Health health))
        {
            _enemy = enemy;
            _enemysHealth = health;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            _enemy = null;
            _enemysHealth = null;
        }
    }
}