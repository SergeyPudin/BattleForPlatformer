using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private Transform[] _wayPoints;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Player _player;
    [SerializeField] private EnemyHealthBar _enemyHealthBarPrefab;

    private void Awake()
    {
        Queue remainingWayPoints = new Queue();

        for (int i = 0; i < _wayPoints.Length; i++)
        {
            remainingWayPoints.Enqueue(_wayPoints[i]);
        }

        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            Transform[] wayPoints = GetWaypoints(remainingWayPoints);

            SpawnEnemy(_spawnPoints[i].position, wayPoints);
        }
    }

    private Transform[] GetWaypoints(Queue remainingWayPoints)
    {
        int waypointQuantity = 2;
        Transform[] wayPoints = new Transform[waypointQuantity];

        for (int i = 0; i < wayPoints.Length; i++)
        {
            wayPoints[i] = (Transform)remainingWayPoints.Dequeue();
        }

        return wayPoints;
    }

    private void SpawnEnemy(Vector3 position, Transform[] waypoints)
    {
        Enemy enemy = Instantiate(_enemyPrefab, position, Quaternion.identity);

        enemy.TryGetComponent(out EnemyMover mover);
        mover.SetWaypoints(waypoints);
        mover.SetPlayer(_player);

        SpawnHealthBar(enemy);
    }

    private void SpawnHealthBar(Enemy enemy)
    {
        if (FindObjectOfType<Canvas>().transform)
        {
            EnemyHealthBar enemyHealthBar = Instantiate(_enemyHealthBarPrefab, transform.position, Quaternion.identity, FindObjectOfType<Canvas>().transform);

            EnemyHealthBarPosition healthBarPosition = enemyHealthBar.GetComponent<EnemyHealthBarPosition>();
            SmoothSliderView smoothSliderView = enemy.GetComponent<SmoothSliderView>();

            healthBarPosition.GetEnemy(enemy);
            smoothSliderView.GetSlider(enemyHealthBar.GetComponent<Slider>());
        }
    }
}