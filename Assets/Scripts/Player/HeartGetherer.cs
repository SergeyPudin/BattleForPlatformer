using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Health))]
public class HeartGetherer : MonoBehaviour
{
    private Health _health;

    public event UnityAction TookHeart;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Heart heart))
        {
            _health.Heal(heart.HealPoints);
            Destroy(heart.gameObject);
            TookHeart?.Invoke();
        }
    }
}