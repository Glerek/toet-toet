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

	public override void StartGameMode()
	{
		_car = Instantiate(_carTemplate, _spawnTarget.transform.position, Quaternion.identity);
	}

	public override void StopGameMode()
	{
		SceneManager.UnloadSceneAsync("AmosTesting");
	}

	public void ToggleRepairMode()
	{
		DuringRepairMode = !DuringRepairMode;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.G))
		{
			GameManager.Instance.StartGameMode(GameManager.GameMode.GameOver);
		}
	}
}