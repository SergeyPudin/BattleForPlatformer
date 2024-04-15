using UnityEngine;

public class EnemyHealthBarPosition : MonoBehaviour
{
    [SerializeField] private float _heightOffset = 1f;

    private Enemy _enemy;

    private void Update()
    {
        if (_enemy != null)
        {
            Vector3 position = _enemy.transform.position;
            position.y += _heightOffset;

            transform.position = position;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GetEnemy(Enemy enemy)
    {
        _enemy = enemy;
    }
}