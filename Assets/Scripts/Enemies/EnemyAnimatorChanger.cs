using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimatorChanger : MonoBehaviour
{
    private const string Running = "Run";    
    private const string Attacking = "Attack";
    private const string Dying = "Die";

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Attack()
    {
        _animator.SetTrigger(Attacking);
    }

    public void Run()
    {
        _animator.SetBool(Running, true);
    }

    public void Die()
    {
        _animator.SetTrigger(Dying);
    }
}