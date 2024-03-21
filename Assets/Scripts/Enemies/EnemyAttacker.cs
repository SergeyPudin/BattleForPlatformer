using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyMover))]
[RequireComponent(typeof(EnemyAnimatorChanger))]
public class EnemyAttacker : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private int _loadTime;

    private EnemyAnimatorChanger _animatorChanger;
    private EnemyMover _mover;
    private Coroutine _attackCoroutine;
    private bool _isAttacking = false;

    public bool IsAttacking => _isAttacking;

    private void Awake()
    {
        _mover = GetComponent<EnemyMover>();
        _animatorChanger = GetComponent<EnemyAnimatorChanger>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider != null)
        {
            if (collision.collider.TryGetComponent(out Health health) && collision.collider.TryGetComponent(out Player player))
            {
                _mover.StopMoving();
                _isAttacking = true;
                _attackCoroutine = StartCoroutine(Attack(health));
            }

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider != null)
        {
            if (collision.collider.TryGetComponent(out Health health) && collision.collider.TryGetComponent(out Player player))
            {
                    StopCoroutine(_attackCoroutine);
                    _animatorChanger.Run();
                    _isAttacking = false;
            }
        }
    }

    private IEnumerator Attack(Health health)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_loadTime);

        while (true)
        {
            health.TakeDamage(_damage);
            _animatorChanger.Attack();

            yield return waitForSeconds;
        }
    }
}