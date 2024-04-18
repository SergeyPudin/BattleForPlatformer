using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Vampirism : MonoBehaviour
{
    [SerializeField] private float _radius = 3.0f;
    [SerializeField] private float _vampirismTime = 6.0f;
    [SerializeField] private float _damagePerSecond = 1.0f / 6;
    [SerializeField] private float _healingKoeficient = 0.5f;

    private Health _health;
    private float _elapsedTime;
    private float _elapsedTimeTakeHealth;
    private Coroutine _coroutine;
    private Coroutine _takeHealth;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q))
            StoleHealth();
    }

    private void Start()
    {
        _health = GetComponent<Health>();
    }

    private void StoleHealth()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _radius);

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out Enemy enemy))
            {
                if (enemy.TryGetComponent(out Health health))
                {
                    float damage = _damagePerSecond * Time.deltaTime;
                    float healPoints = damage * _healingKoeficient;

                    _coroutine = StartCoroutine(SuckHealth(health, damage));
                    _takeHealth = StartCoroutine(TakeHealth(healPoints));
                }
            }
        }
    }

    private IEnumerator TakeHealth(float healPoints)
    {
        _elapsedTimeTakeHealth = _vampirismTime;

        while (_elapsedTimeTakeHealth > 0)
        {
            _health.Heal(healPoints);
            _elapsedTimeTakeHealth -= Time.deltaTime;

            yield return null;
        }

        _takeHealth = null;
    }

    private IEnumerator SuckHealth(Health health, float damage)
    {
        _elapsedTime = _vampirismTime;

        while (_elapsedTime > 0)
        {
            health.TakeDamage(damage);
            _elapsedTime -= Time.deltaTime;

            yield return null;
        }

        _coroutine = null;
    }
}