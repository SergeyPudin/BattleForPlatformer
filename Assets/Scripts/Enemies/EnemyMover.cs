using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(EnemyAttacker))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _chaseRange;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _groundedDistance = 0.1f;

    private Rigidbody2D _rigidbody;
    private Transform[] _waypoints;
    private Player _player;
    private Transform _target;
    private SpriteRenderer _spriteRenderer;
    private EnemyAttacker _enemyAttacker;

    private float _lastPosition;
    private float _lastDirection;

    private bool _isPatroling = true;

    private void Awake()
    {
        int waypointQuantity = 2;

        _waypoints = new Transform[waypointQuantity];

        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _enemyAttacker = GetComponent<EnemyAttacker>();

        _lastPosition = transform.position.x;
    }

    private void FixedUpdate()
    {
        TryChangeTarget();
        ChasePlayerIfNear();

        if (_target != null)
        {
            Vector2 direction = (_target.position - transform.position).normalized;

            if (IsGrounded())
            {
                _rigidbody.velocity = direction * _moveSpeed;
            }
        }

        if (_target != transform)
        {
            TryChangeRotation(_rigidbody.velocity.x);
        }

        _lastPosition = transform.position.x;
    }

    public void SetWaypoints(Transform[] waypoints)
    {
        _waypoints = waypoints;
        SetRandomWaypoint();
    }

    public void SetPlayer(Player player)
    {
        _player = player;
    }

    public void StopMoving()
    {
        _target = transform;
        TryChangeRotation(_lastDirection);
    }

    private void TryChangeRotation(float direction)
    {
        _spriteRenderer.flipX = direction < 0 ? true : false;
        _lastDirection = direction;
    }

    private void TryChangeTarget()
    {
        float tolerance = 0.2f;

        if (_player == null)
        {
            return;
        }

        if (_isPatroling == false && _enemyAttacker.IsAttacking == false)
        {
            _target = _player.transform;
        }

        if (Mathf.Abs(_target.position.x - transform.position.x) < tolerance)
        {
            for (int i = 0; i < _waypoints.Length; i++)
            {
                if (Mathf.Abs(_waypoints[i].position.x - transform.position.x) < tolerance)
                {
                    _target = _waypoints[(i + 1) % _waypoints.Length];
                }
            }
        }
    }

    private void ChasePlayerIfNear()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, _chaseRange);

        if (hit != null && hit.TryGetComponent(out Player player))
            _isPatroling = false;
    }

    private bool IsGrounded()
    {
        bool isGrounded = Physics2D.Raycast(transform.position, Vector2.down, _groundedDistance, _groundLayer);

        return isGrounded;
    }

    private void SetRandomWaypoint()
    {
        int randomIndex = Random.Range(0, _waypoints.Length);
        _target = _waypoints[randomIndex];
    }
}