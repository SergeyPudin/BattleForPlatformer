using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AnimationChanger))]
[RequireComponent(typeof(PlayerMover))]
public class PlayerAttacker : MonoBehaviour
{
    [SerializeField] private Rock _rockPrefab;
    [SerializeField] private float _throwForce;
    [SerializeField] private float _loadTime;

    private AnimationChanger _animationChanger;
    private PlayerMover _playerMover;

    private bool _isLoaded = true;

    private void Awake()
    {
        _playerMover = GetComponent<PlayerMover>();
        _animationChanger = GetComponent<AnimationChanger>();
    }

    private void Update()
    {        
            StartCoroutine(DelayedAttack());
    }

    private void Throw()
    {
        Rock currentRock;

        _animationChanger.Attack();
        currentRock = Instantiate(_rockPrefab, transform.position, Quaternion.identity);
        currentRock.SetVelocity(_throwForce, _playerMover.IsRight);
    }

    private IEnumerator DelayedAttack()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_loadTime);

        if (_isLoaded && Input.GetKeyDown(KeyCode.Space))
        {
            _isLoaded = false;

            Throw();

            yield return waitForSeconds;

            _isLoaded = true;
        }
    }
}