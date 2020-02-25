using System.Collections.Generic;
using UnityEngine;

public class RoadBlock : MonoBehaviour
{
	[SerializeField]
	private Transform _startConnector = null;
	public Transform StartConnector { get { return _startConnector; } }

	[SerializeField]
	private Transform _endConnector = null;
	public Transform EndConnector { get { return _endConnector; } }

	[SerializeField]
	private GameObject Graphics = null;

	[SerializeField]
	private Bezier _bezier = null;

	[SerializeField]
	private Transform _foregroundElementParent = null;

	private RoadGeneratorParameters _parameters = null;
	private List<GameObject> _spawnedForegrounds = new List<GameObject>();

	public void Initialize(RoadGeneratorParameters parameters)
	{
		_parameters = parameters;

		int foregroundSpawnedCount = 0;
		int infinitePreventerCounter = 0;

		while (foregroundSpawnedCount < _parameters.NumberOfRandomForegroundElementsPerBlock)
		{
			GameObject randomElement = _parameters.RandomForegroundElements[Random.Range(0, _parameters.RandomForegroundElements.Length)];
			float randomXPosition = Random.Range(_startConnector.position.x, _endConnector.position.x);
			bool isPositionCorrect = true;
			for (int i = 0; i < _spawnedForegrounds.Count; i++)
			{
				isPositionCorrect &= Mathf.Abs(_spawnedForegrounds[i].transform.position.x - randomXPosition) > _parameters.MinimalSpaceBetweenElements;
			}

			if (isPositionCorrect)
			{
				Vector3 spawnPoint = _bezier.GetPointByXAxis(randomXPosition);
				_spawnedForegrounds.Add(GameObject.Instantiate(randomElement, spawnPoint, Quaternion.identity, _foregroundElementParent));

				infinitePreventerCounter = 0;
				foregroundSpawnedCount++;
			}
			else
			{
				infinitePreventerCounter++;
				if (infinitePreventerCounter > 999)	{ break; }
			}
		}
	}

	public void OnEndBlockCrossed(GameObject trigger, Collider2D collider)
	{
		if (string.Equals(collider.tag, "Car"))
		{
			RoadGenerator.Instance.OnEndOfBlockCrossed(this);
		}
	}
}