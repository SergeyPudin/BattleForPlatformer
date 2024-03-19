using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _moveSpeed;

    private Vector3 _targetPosition;

    private void Start()
    {
        _targetPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (_player != null)
        {
            _targetPosition.x = _player.transform.position.x;

            transform.position = Vector3.Lerp(transform.position, _targetPosition, _moveSpeed);
        }
    }
}