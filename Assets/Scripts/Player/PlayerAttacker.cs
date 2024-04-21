using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AnimationChanger), typeof(PlayerMover))]
public class PlayerAttacker : MonoBehaviour
{
    [SerializeField] private Rock _rockPrefab;
    [SerializeField] private float _throwForce;
    [SerializeField] private float _loadTime;

    private AnimationChanger _animationChanger;
    private PlayerMover _playerMover;
    private Coroutine _attackCoroutine;

    private bool _isLoaded = true;

    private void Awake()
    {
        _playerMover = GetComponent<PlayerMover>();
        _animationChanger = GetComponent<AnimationChanger>();
    }

    public void Throw()
    {
        _attackCoroutine = StartCoroutine(DelayedAttack());
    }

    private void CreateRock()
    {
        Rock currentRock;

        currentRock = Instantiate(_rockPrefab, transform.position, Quaternion.identity);
        currentRock.SetVelocity(_throwForce, _playerMover.IsRight);
    }

    private IEnumerator DelayedAttack()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_loadTime);

        if (_isLoaded)
        {
            _isLoaded = false;

            CreateRock();
            _animationChanger.Attack();

            yield return waitForSeconds;

            _isLoaded = true;
        }

        _attackCoroutine = null;
    }
}