using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
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

				RepairingUI.Instance.Display(_duringRepairMode);
			}
		}
	}

    void Awake()
    {
        _car = Instantiate(_carTemplate, _spawnTarget.transform.position, Quaternion.identity);
    }

	private void Start()
	{
		RepairingUI.Instance.Display(false);

		// for (int i = 0; i < 3; i++)
		// {
		// 	Wheel spawnedWheel = GameObject.Instantiate(_wheelTemplate, new Vector3(-30, -2, 0), Quaternion.identity);
		// 	_spawnedWheels.Add(spawnedWheel);
		// 	InventoryManager.Instance.AddToInventory(spawnedWheel);
		// }
	}

	public void ToggleRepairMode()
	{
		DuringRepairMode = !DuringRepairMode;
	}
}
