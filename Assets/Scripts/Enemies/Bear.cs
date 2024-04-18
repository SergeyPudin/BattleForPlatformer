using UnityEngine;

[RequireComponent(typeof(Health), typeof(EnemyAnimatorChanger), typeof(EnemyMover))]
public class Bear : MonoBehaviour
{
    private Health _health;
    private EnemyAnimatorChanger _changer;
    private EnemyMover _mover;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _changer = GetComponent<EnemyAnimatorChanger>();
        _mover = GetComponent<EnemyMover>();
    }

    private void OnEnable()
    {
        _health.Dyed += OnDying;
    }

    private void OnDisable()
    {
        _health.Dyed -= OnDying;
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDying()
    {
        _mover.StopMoving();
        _changer.Die();
    }
}