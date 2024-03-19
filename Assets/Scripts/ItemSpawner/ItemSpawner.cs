using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private Item[] _itemPrefab;
    [SerializeField] private Transform _spawnerPointConteiner;
    [SerializeField] private DiamondGatherer _diamonGatherer;
    [SerializeField] private HeartGetherer _heartGetherer;

    private Queue<Transform> _spawnerPoints = new Queue<Transform>();

    private void Awake()
    {
        for (int i = 0; i < _spawnerPointConteiner.childCount; i++)
        {
            _spawnerPoints.Enqueue(_spawnerPointConteiner.GetChild(i));
        }
    }

    private void Start()
    {
        CreateItem();
    }

    private void OnEnable()
    {
        _diamonGatherer.TookDiamond += CreateItem;
        _heartGetherer.TookHeart += CreateItem;
    }

    private void OnDisable()
    {
        _diamonGatherer.TookDiamond -= CreateItem;
        _heartGetherer.TookHeart -= CreateItem;
    }

    private void CreateItem()
    {
        if (_spawnerPoints.Count != 0)
            Instantiate(RandomItem(), _spawnerPoints.Dequeue());
    }

    private Item RandomItem()
    {
        int randomIndex = Random.Range(0, _itemPrefab.Length);

        return _itemPrefab[randomIndex];
    }
}