using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField]
    private PickableObject _objectTemplate = null;

    [SerializeField]
    private SpawnTable _spawnTable = null;

    [Header("Spawn parameters (timer are in seconds)")]
    [SerializeField]
    private float _delayBetweenSpawns = 10f;

    private Coroutine _spawnCoroutine = null;
    private List<PickableObject> _spawnedObjects = new List<PickableObject>();

    private void Start()
    {
        // _spawnCoroutine = StartCoroutine(SpawnCoroutine());

        // Debug.Log(Camera.main.name);
        // Vector3 carPosition = GameManager.Instance.Car.transform.position;
        // SpawnObjectAtWorldPosition(Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, carPosition.z)));
    }

    private void OnDestroy()
    {
        if (_spawnCoroutine != null)
        {
            StopCoroutine(_spawnCoroutine);
        }
    }

    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            SpawnObject();

            yield return new WaitForSeconds(_delayBetweenSpawns);
        }
    }

    private void SpawnObject()
    {
        SubsystemData data = _spawnTable.GetRandomData();

        if (data != null)
        {
            Vector3 carPosition = GameManager.Instance.Car.transform.position;
            Vector3 spawnPos = Camera.main.ViewportToWorldPoint(new Vector3(1.5f, 0.5f, 0f));
            spawnPos.z = carPosition.z;
            
            PickableObject spawnedObject = GameObject.Instantiate(_objectTemplate, transform);
            spawnedObject.transform.position = spawnPos;
            spawnedObject.Initialize(data);

            _spawnedObjects.Add(spawnedObject);

            Debug.Log($"<color=green>Spawned {data.Name} at position {spawnPos}</color>");
        }
    }

    private void SpawnObjectAtWorldPosition(Vector3 worldPosition)
    {
        SubsystemData data = _spawnTable.GetRandomData();

        if (data != null)
        {
            PickableObject spawnedObject = GameObject.Instantiate(_objectTemplate, worldPosition, Quaternion.identity, transform);
            spawnedObject.Initialize(data);

            _spawnedObjects.Add(spawnedObject);

            Debug.Log($"<color=green>Spawned {data.Name} at position {worldPosition}</color>");
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
