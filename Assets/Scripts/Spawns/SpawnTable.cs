using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "SpawnTable", menuName = "ScriptableObjects/Spawn/Create Spawn Table", order = 1)]
public class SpawnTable : ScriptableObject
{
    [Serializable]
    public class Entry
    {
        public SubsystemData Data;
        public int Weight;
    }

    [SerializeField]
    private List<Entry> _table = new List<Entry>();

    private int _totalWeight = int.MaxValue;

    private void OnValidate()
    {
        if (_totalWeight != int.MaxValue)
            _totalWeight = int.MaxValue;
    }

    public SubsystemData GetRandomData()
    {
        if (_totalWeight == int.MaxValue)
            ComputeTotalWeight();

        int random = UnityEngine.Random.Range(0, _totalWeight);
        int cumulative = 0;

        for (int i = 0; i < _table.Count; i++)
        {
            cumulative += _table[i].Weight;
            if (cumulative > random)
            {
                return _table[i].Data;
            }
        }

        return null;
    }

    private void ComputeTotalWeight()
    {
        _totalWeight = 0;
        for (int i = 0; i < _table.Count; i++)
        {
            _totalWeight += _table[i].Weight;
        }
    }
}
