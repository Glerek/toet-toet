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

    private PlayerAction.PlayerActions _input;
    private Car _car;
    private bool _pushedAcceleration = false;

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
	}

    void OnEnable() => _input.Enable();
    void OnDestroy() => _input.Disable();

    void OnDisable() => _input.Disable();

    // Update is called once per frame
    void Update()
    {
		_car.HandleMovement(_pushedAcceleration);
    }

    public void OnAccel(InputAction.CallbackContext context)
    {
        _pushedAcceleration = context.ReadValue<float>() == 1.0f;
    }
}
