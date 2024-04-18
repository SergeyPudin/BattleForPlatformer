using UnityEngine;

[RequireComponent(typeof(Health), typeof(AnimationChanger))]
public class Player : MonoBehaviour 
{
    private Health _health;
    private AnimationChanger _changer;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _changer = GetComponent<AnimationChanger>();
    }

    private void OnEnable()
    {
        _health.Dyed += OnDying;
    }

    private void OnDisable()
    {
        _health.Dyed -= OnDying;
    }

    private void DestroyPlayer()
    {
        Destroy(gameObject);
    }

    private void OnDying()
    {
        _changer.Die();
    }
}