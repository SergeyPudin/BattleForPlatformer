using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Rock : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _angle = 35f;

    private Vector2 _velocity;
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.AddForce(_velocity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent(out DamageTaker damageTaker))
        {
            damageTaker.TryGetComponent(out Health health);
            health.TakeDamage(_damage);
            Destroy(gameObject);
        }

        if (collision.transform.TryGetComponent(out Ground ground))
        {
            Destroy(gameObject);
        }
    }

    public void SetVelocity(float throwForce, bool isRight)
    {
        Vector2 forceDirection = Quaternion.AngleAxis(_angle, Vector3.forward) * Vector2.right;
       
        _velocity = new Vector2(GetDirection(isRight), 1) * throwForce * forceDirection;
    }

    private float GetDirection(bool isRight)
    {
        int right = 1;
        int left = -1;

        return isRight ? right : left;
    }
}