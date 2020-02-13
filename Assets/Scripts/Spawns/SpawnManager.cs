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

    [SerializeField]
    private float _distanceMinimumBetweenSpawns = 20f;

    private Coroutine _spawnCoroutine = null;
    private List<PickableObject> _spawnedObjects = new List<PickableObject>();
    private List<PickableObject> _nearbyObjects = new List<PickableObject>();

    private void Start()
    {
        _spawnCoroutine = StartCoroutine(SpawnCoroutine());
    }

    private void OnDestroy()
    {
        if (_spawnCoroutine != null)
        {
            StopCoroutine(_spawnCoroutine);
        }

        for (int i = 0; i < _spawnedObjects.Count; i++)
        {
            GameObject.Destroy(_spawnedObjects[i].gameObject);
        }
        _spawnedObjects.Clear();
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
            Vector3 carPosition = (GameManager.Instance.CurrentGameMode as DrivingMode).Car.transform.position;
            Vector3 spawnPos = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(1.1f, 1.6f), 1.5f, Mathf.Abs(carPosition.z - Camera.main.transform.position.z)));

            if (ChekSpawnDistance(spawnPos))
            {
                RaycastHit2D hit = Physics2D.Raycast(spawnPos, Vector2.down, LayerMask.GetMask("Road"));
                if (hit.collider != null)
                {
                    spawnPos.y = hit.point.y;
                }

                PickableObject spawnedObject = GameObject.Instantiate(_objectTemplate, spawnPos, Quaternion.identity, transform);
                spawnedObject.Initialize(data);
                _spawnedObjects.Add(spawnedObject);

                Debug.Log($"<color=green>Spawned {data.Name} at position {spawnPos}</color>");
            }
        }
    }

    private bool ChekSpawnDistance(Vector3 spawnPosition)
    {
        PickableObject lastSpawnedObject = _spawnedObjects.Count > 0 ? _spawnedObjects[_spawnedObjects.Count - 1] : null;

        // TODO: Improve that in case we're going backward
        if (lastSpawnedObject != null)
        {
            return Mathf.Abs(spawnPosition.x - lastSpawnedObject.transform.position.x) > _distanceMinimumBetweenSpawns;
        }
        return true;
    }

    public void PickupNearbyObjects()
    {
        for (int i = _nearbyObjects.Count - 1; i >= 0; i--)
        {
            PickupObject(_nearbyObjects[i]);
        }
    }

    public bool PickupObject(PickableObject obj)
    {
        if (InventoryManager.Instance.AddToInventory(obj.Data))
        {
            _spawnedObjects.Remove(obj);
            GameObject.Destroy(obj.gameObject);

            return true;
        }

        return false;
    }

    public void RegisterPickable(PickableObject obj)
    {
        if (_nearbyObjects.Contains(obj) == false)
        {
            _nearbyObjects.Add(obj);
        }
    }

    public void UnregisterPickable(PickableObject obj)
    {
        _nearbyObjects.Remove(obj);
    }
}
