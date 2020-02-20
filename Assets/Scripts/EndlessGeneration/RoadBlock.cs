using UnityEngine;

public class RoadBlock : MonoBehaviour
{
	[SerializeField]
	private Transform _startConnector = null;
	public Transform StartConnector { get { return _startConnector; } }

	[SerializeField]
	private Transform _endConnector = null;
	public Transform EndConnector { get { return _endConnector; } }
}