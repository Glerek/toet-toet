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

	[SerializeField]
	private GameObject _fogGameObject = null;

	private RoadGeneratorParameters _parameters = null;
	private List<ForegroundElement> _spawnedForegrounds = new List<ForegroundElement>();

	public void Initialize(RoadGeneratorParameters parameters)
	{
		_parameters = parameters;

		SpawnForegroundElements();
		RandomizeFog();
	}

	private void SpawnForegroundElements()
	{
		int foregroundSpawnedCount = 0;
		int infinitePreventerCounter = 0;

		while (foregroundSpawnedCount < _parameters.NumberOfRandomForegroundElementsPerBlock)
		{
			ForegroundElement randomElement = _parameters.RandomForegroundElements[Random.Range(0, _parameters.RandomForegroundElements.Length)];
			float randomXPosition = Random.Range(_startConnector.position.x, _endConnector.position.x);
			Vector3 spawnPoint = _bezier.GetPointByXAxis(randomXPosition);

			bool isPositionCorrect = true;
			for (int i = 0; i < _spawnedForegrounds.Count; i++)
			{
				isPositionCorrect &= _spawnedForegrounds[i].Renderer.bounds.Intersects(new Bounds(spawnPoint + (randomElement.Renderer.transform.position - randomElement.transform.position), randomElement.Renderer.bounds.size)) == false;
			}

			if (isPositionCorrect)
			{
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

	private void RandomizeFog()
	{
		bool enableFog = Random.Range(0,4) == 0;

		_fogGameObject.SetActive(enableFog);

		if (enableFog)
		{
			Vector3 fogPosition = _fogGameObject.transform.position;
			fogPosition.x = Random.Range(0, _endConnector.position.x);

			_fogGameObject.transform.position = fogPosition;
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