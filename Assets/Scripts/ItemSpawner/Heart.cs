using UnityEngine;

public class Heart : Item
{
    [SerializeField] private int _healPoints;

    public int HealPoints => _healPoints;
}