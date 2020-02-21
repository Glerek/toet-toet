using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : Singleton<RoadGenerator>
{
	[SerializeField]
	private RoadGeneratorParameters _parameters = null;

	private List<RoadBlock> _blocks = new List<RoadBlock>();

	private void Awake()
	{
		_blocks.Add(SpawnBlock(Vector3.zero, Vector3.zero));
	}

	private RoadBlock SpawnBlock(Vector3 position, Vector3 rotation)
	{
		RoadBlock newBlock = GameObject.Instantiate(_parameters.RoadBlock, position, Quaternion.Euler(rotation), transform);
		newBlock.Initialize();

		return newBlock;
	}

	private Vector3 _latestSpawnVectorA;
	private Vector3 _latestSpawnVectorB;
	public void OnEndOfBlockCrossed(RoadBlock block)
	{
		// Vector3 newSpawnPosition = block.EndConnector.transform.position + (block.transform.position - block.StartConnector.transform.position);
		Vector3 newSpawnPosition = block.transform.position - block.StartConnector.transform.position;
		float randomAngle = Random.Range(-20f, 20f);
		Debug.Log("Random angle: " + randomAngle);
		newSpawnPosition = Quaternion.AngleAxis(randomAngle, Vector3.forward) * newSpawnPosition;
		_latestSpawnVectorB = newSpawnPosition;
		_latestSpawnVectorA = block.EndConnector.transform.position;

		_blocks.Add(SpawnBlock(block.EndConnector.transform.position + newSpawnPosition, new Vector3(0, 0, randomAngle)));
	}

	private void Update()
	{
		Debug.DrawLine(_latestSpawnVectorA, _latestSpawnVectorB, Color.red, 1f);
	}
}
