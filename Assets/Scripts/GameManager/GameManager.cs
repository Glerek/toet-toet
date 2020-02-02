using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : Singleton<GameManager>, PlayerAction.IPlayerActions
{
	[SerializeField]
	private GameObject _spawnTarget = null;

	[SerializeField]
    private Car _carTemplate = null;

	[SerializeField]
	private Wheel _wheelTemplate = null;

	[SerializeField]
	private float _collisionMagnitudeThreshold = 0f;
	public float CollisionMagnitudeThreshold { get { return _collisionMagnitudeThreshold; } }

    private PlayerAction.PlayerActions _input;
    private Car _car;
    private bool _pushedAcceleration = false;
    private bool _pushedBreak = false;

	private List<Wheel> _spawnedWheels = new List<Wheel>();

	public Car Car
	{
		get { return _car; }
	}

    void Awake()
    {
        _input = new PlayerAction.PlayerActions(new PlayerAction());
        _input.SetCallbacks(this);

        _car = Instantiate(_carTemplate, _spawnTarget.transform.position, Quaternion.identity);
    }

	private void Start()
	{
		DrivingUI.Instance.Display(true);
		RepairingUI.Instance.Display(false);

		for (int i = 0; i < 3; i++)
		{
			Wheel spawnedWheel = GameObject.Instantiate(_wheelTemplate, new Vector3(-30, -2, 0), Quaternion.identity);
			_spawnedWheels.Add(spawnedWheel);
			InventoryManager.Instance.AddToInventory(spawnedWheel);
		}
	}

    void OnEnable() => _input.Enable();
    void OnDestroy() => _input.Disable();

    void OnDisable() => _input.Disable();

    void Update()
    {
        _car.Accel(_pushedAcceleration);
        _car.Brake(_pushedBreak);
    }

    public void OnAccel(InputAction.CallbackContext context)
    {
        //space 
        _pushedAcceleration = context.ReadValue<float>() == 1.0f;
    }

    public void OnBreak(InputAction.CallbackContext context)
    {
        //left control
        _pushedBreak = context.ReadValue<float>() == 1.0f;
    }
}
