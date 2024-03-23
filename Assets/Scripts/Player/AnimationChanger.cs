using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationChanger : MonoBehaviour
{
    private const string Running = "Run";
    private const string Jumping = "Jump";
    private const string Attacking = "Attack";

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Run()
    {
        _animator.SetBool(Running, true);
    }

    public void Idle()
    {
        _animator.SetBool(Running, false);
    }

    public void Jump()
    {
        _animator.SetTrigger(Jumping);
    }

    public void Attack()
    {
        _animator.SetTrigger(Attacking);
    }
}