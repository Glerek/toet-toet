using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : Singleton<RoadGenerator>
{
	[SerializeField]
	private RoadGeneratorParameters _parameters = null;

	private List<RoadBlock> _blocks = new List<RoadBlock>();
	private float _currentGlobalAngle = 0f;

	private void Awake()
	{
		// Change initial spawn to prevent going BACKWARDS
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
		_currentGlobalAngle= Random.Range(
									Mathf.Max(-_parameters.MaxRoadAngle, _currentGlobalAngle - _parameters.BlockRandomAngleCap),
									Mathf.Min(_parameters.MaxRoadAngle, _currentGlobalAngle + _parameters.BlockRandomAngleCap));

		_blocks.Add(SpawnBlock(block.EndConnector.transform.position, new Vector3(0, 0, _currentGlobalAngle)));
	}
}
