using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationChanger : MonoBehaviour
{
    private const int IdleIndex = 0;
    private const int RunIndex = 1;
    private const int JumpIndex = 2;
    private const int AttackIndex = 3;

    private int _moveIndex = Animator.StringToHash("MoveIndex");   
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Run()
    {
        _animator.SetInteger(_moveIndex, RunIndex);
    }

    public void Jump()
    {
        _animator.SetInteger(_moveIndex, JumpIndex);
    }

    public void Idle()
    {
        _animator.SetInteger(_moveIndex, IdleIndex);
    }

    public void Attack()
    {
        _animator.SetInteger(_moveIndex, AttackIndex);
    }
}