using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField]
    private PickableObject _objectTemplate = null;

    [SerializeField]
    private SpawnTable _spawnTable = null;

    private List<PickableObject> _spawnedObjects = new List<PickableObject>();

    private void Start()
    {
        
    }

    private void SpawnObject()
    {
        SubsystemData data = _spawnTable.GetRandomData();

        if (data != null)
        {
            // Spawn
        }
    }

    public void PickupObject(PickableObject obj)
    {
        if (InventoryManager.Instance.AddToInventory(obj.Data))
        {
            _spawnedObjects.Remove(obj);
            GameObject.Destroy(obj.gameObject);
        }
    }
}
