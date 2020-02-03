using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	[SerializeField]
	private GameObject _spawnTarget = null;

	[SerializeField]
    private Car _carTemplate = null;

	// [SerializeField]
	// private Wheel _wheelTemplate = null;

	[SerializeField]
	private float _collisionMagnitudeThreshold = 0f;
	public float CollisionMagnitudeThreshold { get { return _collisionMagnitudeThreshold; } }

    private Car _car;

	// private List<Wheel> _spawnedWheels = new List<Wheel>();

	public Car Car
	{
		get { return _car; }
	}

    void Awake()
    {
        _car = Instantiate(_carTemplate, _spawnTarget.transform.position, Quaternion.identity);
    }

	private void Start()
	{
		DrivingUI.Instance.Display(true);
		RepairingUI.Instance.Display(false);

		// for (int i = 0; i < 3; i++)
		// {
		// 	Wheel spawnedWheel = GameObject.Instantiate(_wheelTemplate, new Vector3(-30, -2, 0), Quaternion.identity);
		// 	_spawnedWheels.Add(spawnedWheel);
		// 	InventoryManager.Instance.AddToInventory(spawnedWheel);
		// }
	}
}
