using UnityEngine;
using UnityEngine.SceneManagement;

public class DrivingMode : IGameMode
{
	[SerializeField]
	private GameObject _spawnTarget = null;

	[SerializeField]
	private Car _carTemplate = null;

	[SerializeField]
	private SubsystemIconData _iconData = null;
	public SubsystemIconData IconData { get { return _iconData; } }

	[SerializeField]
	private GameFinishUI _finishGameUI = null;

	[SerializeField]
	private RoadGenerator _roadGenerator = null;

	private Car _car;
	public Car Car { get { return _car; } }

	private bool _duringRepairMode = false;
	public bool DuringRepairMode
	{
		get { return _duringRepairMode; }
		private set
		{
			if (_duringRepairMode != value)
			{
				_duringRepairMode = value;

				int layerMask = ~0;
				if (_duringRepairMode)
				{
					layerMask = ~LayerMask.GetMask(new string[] { "UI", "Car", "Wheel" });
				}

				Camera.main.cullingMask = layerMask;
				_car.SetRepairMode(_duringRepairMode);
			}
		}
	}

	private void OnGameFinished()
	{
		GameObject.Destroy(_car.gameObject);
		GameManager.Instance.StartGameMode(GameManager.GameMode.GameOver);
	}

	public void FinishDrivingMode(bool victory)
	{
		DuringRepairMode = false;
		Car.FreezeCar(true);
		_finishGameUI.Play(victory, OnGameFinished);
	}

	private void OnCarMovementChanged(bool isSleeping)
	{
		if (isSleeping && _car != null)
		{
			// Game over if (AND):
			// * Both wheels are broken
			// * No wheel in inventory
			// * No wheel visible on the screen
			
			bool bothWheelsBroken = AreAllWheelsBroken();
			bool noWheelInInventory = InventoryManager.Instance.Inventory.FindAll(item => item.Type == SubsystemData.SubsystemType.Wheel).Count <= 0;
			bool noWheelsOnScreen = IsThereSpawnedWheelsOnScreen() == false;

			if (bothWheelsBroken && noWheelInInventory && noWheelsOnScreen)
			{
				FinishDrivingMode(false);
			}
		}
	}

	private void OnCarStuck()
	{
		FinishDrivingMode(false);
	}

	private bool AreAllWheelsBroken()
	{
		bool result = true;
		for (int i = 0; i < _car.Wheels.Count; i++)
		{
			result &= _car.Wheels[i].Wheel == null;
		}

		return result;
	}

	private bool IsThereSpawnedWheelsOnScreen()
	{
		bool result = false;
		Plane[] cameraPlanes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
		for (int i = 0; i < SpawnManager.Instance.SpawnedObjects.Count; i++)
		{
			result |= GeometryUtility.TestPlanesAABB(cameraPlanes, SpawnManager.Instance.SpawnedObjects[i].GetComponent<Collider2D>().bounds);
		}

		return result;
	}

	public override void StartGameMode()
	{
		if (_roadGenerator != null)
		{
			_roadGenerator.Initialize();
		}

		_car = Instantiate(_carTemplate, _spawnTarget.transform.position, Quaternion.identity);

		_car.OnCarMovementChanged += OnCarMovementChanged;
		_car.OnCarStuck += OnCarStuck;
	}

	public override void StopGameMode()
	{
		SceneManager.UnloadSceneAsync("EndlessDriving");
	}

	public void ToggleRepairMode()
	{
		DuringRepairMode = !DuringRepairMode;
	}
}