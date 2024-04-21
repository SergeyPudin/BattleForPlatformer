using UnityEngine;

[RequireComponent(typeof(Vampirism))]
public class PlayerInput : MonoBehaviour
{
    private Vampirism _vampirism;

    private void Start()
    {
        _vampirism = GetComponent<Vampirism>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q) && _vampirism != null)
            _vampirism.enabled = true;
    }
}