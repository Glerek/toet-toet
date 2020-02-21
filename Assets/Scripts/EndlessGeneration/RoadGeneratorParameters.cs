using UnityEngine;

[CreateAssetMenu(fileName = "RoadGeneratorParameters", menuName = "ScriptableObjects/RoadGeneration/Create Road generation", order = 1)]
public class RoadGeneratorParameters : ScriptableObject
{
	[SerializeField]
	private RoadBlock _roadBlock = null;
	public RoadBlock RoadBlock { get { return _roadBlock; } }
}