using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimatorChanger : MonoBehaviour
{
    private const int _runIndex = 0;    
    private const int _attackIndex = 1;

    private int _actionIndex = Animator.StringToHash("ActionIndex");    
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Attack()
    {
        _animator.SetInteger(_actionIndex, _attackIndex);
    }

    public void Run()
    {
        _animator.SetInteger(_actionIndex, _runIndex);
    }
}