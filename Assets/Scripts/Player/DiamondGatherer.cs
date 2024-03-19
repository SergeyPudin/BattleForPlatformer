using UnityEngine;
using UnityEngine.Events;

public class DiamondGatherer : MonoBehaviour
{
    public event UnityAction TookDiamond;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Diamond diamond))
        {
            Destroy(diamond.gameObject);
            TookDiamond?.Invoke();
        }
    }
}