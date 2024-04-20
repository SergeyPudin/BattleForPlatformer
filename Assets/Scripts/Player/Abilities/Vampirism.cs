using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Vampirism : MonoBehaviour
{
    [SerializeField] private float _vampirismRadius = 3.0f;
    [SerializeField] private float _vampirismTime = 6.0f;
    [SerializeField] private float _damagePerSecond = 1.0f / 6;
    [SerializeField] private float _healingCoefficient = 0.5f;

    private float _elapsedVampirismTime;
    private float _elapsedHealingTime;
    
    private Coroutine _vampirismCoroutine;
    private Coroutine _healingCoroutine;
    private Health _health;

    private void Start()
    {
        _health = GetComponent<Health>();
    }

    public void PerformVampirism()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _vampirismRadius);

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out Enemy enemy))
            {
                if (enemy.TryGetComponent(out Health health))
                {
                    float damage = _damagePerSecond * Time.fixedDeltaTime;
                    float healPoints = damage * _healingCoefficient;

                    _vampirismCoroutine = StartCoroutine(InflictDamage(health, damage));
                    _healingCoroutine = StartCoroutine(TakeHealth(healPoints));
                }
            }
        }
    }

    private IEnumerator TakeHealth(float healPoints)
    {
        _elapsedHealingTime = _vampirismTime;

        while (_elapsedHealingTime > 0)
        {
            _health.Heal(healPoints);

            _elapsedHealingTime -= Time.fixedDeltaTime; 

            yield return new WaitForFixedUpdate();
        }

        _healingCoroutine = null;
    }

    private IEnumerator InflictDamage(Health health, float damage)
    {
        _elapsedVampirismTime = _vampirismTime;

        while (_elapsedVampirismTime > 0)
        {
            health.TakeDamage(damage);
            _elapsedVampirismTime -= Time.fixedDeltaTime;

            yield return new WaitForFixedUpdate();
        }

        _vampirismCoroutine = null;
    }
}