using UnityEngine;

[RequireComponent(typeof(AnimationChanger), typeof(Rigidbody2D), typeof(SpriteRenderer))]
[RequireComponent(typeof(Vampirism), typeof(PlayerAttacker))]
public class PlayerMover : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";

    [Header("Ground Settings")]
    [SerializeField] private float _radiusGround;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private Transform _groundWaypoint;

    [Header("Move Settings")]
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _jumpForce;

    private Vampirism _vampirism;
    private PlayerAttacker _playerAttacker;
    private AnimationChanger _animationChanger;
    private Rigidbody2D _rigidbody;
    private bool _isGrounded = true;
    private SpriteRenderer _spriteRenderer;
    private bool _isRight = true;

    public bool IsRight => _isRight;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animationChanger = GetComponent<AnimationChanger>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _vampirism = GetComponent<Vampirism>();
        _playerAttacker = GetComponent<PlayerAttacker>();
    }

    private void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundWaypoint.position, _radiusGround, _groundMask);

        Run();
        Jump();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q) && _vampirism != null)
            _vampirism.enabled = true;

        if (Input.GetKeyUp(KeyCode.Space))
            _playerAttacker.Throw();
    }

    private void Jump()
    {
        float verticalDirection = Input.GetAxisRaw(Vertical);

        if (_isGrounded && verticalDirection > 0)
        {
            _animationChanger.Jump();
            _rigidbody.AddForce(Vector2.up * _jumpForce);
        }
    }

    private void Run()
    {
        float horizontalDirection = Input.GetAxis(Horizontal);
        var velocity = new Vector3(horizontalDirection * _runSpeed, _rigidbody.velocity.y);

        TryChangeDirection(horizontalDirection);

        _rigidbody.velocity = velocity;

        if (_isGrounded)
        {
            if (horizontalDirection != 0)
                _animationChanger.Run();
            else
                _animationChanger.Idle();
        }
    }

    private void TryChangeDirection(float direction)
    {
        if (direction < 0)
        {
            _isRight = false;
            _spriteRenderer.flipX = true;
        }
        else if (direction > 0)
        {
            _isRight = true;
            _spriteRenderer.flipX = false;
        }
    }
}