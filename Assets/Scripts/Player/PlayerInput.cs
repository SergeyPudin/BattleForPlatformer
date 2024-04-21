using UnityEngine;

[RequireComponent(typeof(Vampirism), typeof(PlayerAttacker))]
public class PlayerInput : MonoBehaviour
{
    private Vampirism _vampirism;
    private PlayerAttacker _playerAttacker;

    private void Start()
    {
        _vampirism = GetComponent<Vampirism>();
        _playerAttacker = GetComponent<PlayerAttacker>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q) && _vampirism != null)
            _vampirism.enabled = true;

        if (Input.GetKeyUp(KeyCode.Space))
            _playerAttacker.Throw();
    }
}