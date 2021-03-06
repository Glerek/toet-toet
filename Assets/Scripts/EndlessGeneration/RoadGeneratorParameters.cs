using UnityEngine;

[CreateAssetMenu(fileName = "RoadGeneratorParameters", menuName = "ScriptableObjects/RoadGeneration/Create Road generation", order = 1)]
public class RoadGeneratorParameters : ScriptableObject
{
	[SerializeField]
	private RoadBlock _roadBlock = null;
	public RoadBlock RoadBlock { get { return _roadBlock; } }

	[SerializeField]
	private float _maxRoadAngle = 20f;
	public float MaxRoadAngle { get { return _maxRoadAngle; } }

	[SerializeField]
	private float _blockRandomAngleCap = 10f;
	public float BlockRandomAngleCap { get { return _blockRandomAngleCap; } }

	[SerializeField]
	private int _displayedAmountOfBlocks = 5;
	public int DisplayedAmountOfBlocks { get { return _displayedAmountOfBlocks; } }

	[SerializeField]
	private int _numberOfRandomForegroundElementsPerBlock = 5;
	public int NumberOfRandomForegroundElementsPerBlock { get { return _numberOfRandomForegroundElementsPerBlock; } }

	[SerializeField]
	private ForegroundElement[] _randomForegroundElements = null;
	public ForegroundElement[] RandomForegroundElements { get { return _randomForegroundElements; } }
}