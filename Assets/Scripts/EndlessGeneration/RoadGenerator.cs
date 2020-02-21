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

	public void OnEndOfBlockCrossed(RoadBlock block)
	{
		Vector3 newSpawnPosition = block.EndConnector.transform.position + (block.transform.position - block.StartConnector.transform.position);

		_blocks.Add(SpawnBlock(newSpawnPosition, Vector3.zero));
	}
}
