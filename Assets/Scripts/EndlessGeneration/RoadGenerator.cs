using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : Singleton<RoadGenerator>
{
	[SerializeField]
	private RoadGeneratorParameters _parameters = null;

	[SerializeField]
	private RoadBlock _initialBlock = null;

	// private List<RoadBlock> _blocks = new List<RoadBlock>();
	private float _currentGlobalAngle = 0f;

	private int NumberOfBlocksBySide { get { return (int)Mathf.Ceil((_parameters.DisplayedAmountOfBlocks - 1f) / 2f); }}

	private void Awake()
	{
		OnEndOfBlockCrossed(_initialBlock);

		// _blocks.Add(_initialBlock);

		// for (int i = 0; i < NumberOfBlocksBySide; i++)
		// {
		// 	RoadBlock block = _blocks[_blocks.Count - 1];
		// 	block.NextBlock = OnEndOfBlockCrossed(block);
		// }
	}

	private RoadBlock SpawnBlock(RoadBlock lastBlock, Vector3 position, Vector3 rotation)
	{
		RoadBlock newBlock = GameObject.Instantiate(_parameters.RoadBlock, position, Quaternion.Euler(rotation), transform);
		newBlock.Initialize(_parameters, lastBlock);

		return newBlock;
	}

	private RoadBlock SpawnAfterBlock(RoadBlock block)
	{
		_currentGlobalAngle= Random.Range(
									Mathf.Max(-_parameters.MaxRoadAngle, _currentGlobalAngle - _parameters.BlockRandomAngleCap),
									Mathf.Min(_parameters.MaxRoadAngle, _currentGlobalAngle + _parameters.BlockRandomAngleCap));

		return SpawnBlock(block, block.EndConnector.transform.position, new Vector3(0, 0, _currentGlobalAngle));
	}

	public void OnEndOfBlockCrossed(RoadBlock block)
	{
		RoadBlock previousBlock = block.PreviousBlock;
		int blockCount = 0;
		while (previousBlock != null)
		{
			previousBlock.EnableGraphics(blockCount < NumberOfBlocksBySide);
			previousBlock = previousBlock.PreviousBlock;
			blockCount++;
		}

		RoadBlock nextBlock = block.NextBlock;
		blockCount = 0;

		while (nextBlock != null || blockCount < NumberOfBlocksBySide)
		{
			if (blockCount < NumberOfBlocksBySide)
			{
				if (nextBlock != null)
				{
					nextBlock.EnableGraphics(true);
				}
				else
				{
					nextBlock = SpawnAfterBlock(block);
					block.NextBlock = nextBlock;
					block = nextBlock;
					// spawnedBlocks.Add(nextBlock);
				}
			}
			else
			{
				nextBlock.EnableGraphics(false);
			}

			nextBlock = nextBlock.NextBlock;
			blockCount++;
		}
		
		// return spawnedBlocks.ToArray();
	}
}
