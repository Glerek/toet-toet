using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>, PlayerAction.IPlayerActions
{
	private PlayerAction.PlayerActions _input;
	private float _accelerateValue = 0f;
	private float _brakeValue = 0f;

	public void OnAccelerate(InputAction.CallbackContext context)
	{
		_accelerateValue = context.ReadValue<float>();
	}

	public void OnBrake(InputAction.CallbackContext context)
	{
		_brakeValue = context.ReadValue<float>();
	}

	public void OnSubsystemDurability(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Performed ||
			context.phase == InputActionPhase.Canceled)
		{
			(GameManager.Instance.CurrentGameMode as DrivingMode).Car.SubsystemUI.Display(context.phase == InputActionPhase.Performed);
		}
	}

	public void OnToggleRepair(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Performed)
		{
			if (context.ReadValue<float>() == 1.0f)
			{
				(GameManager.Instance.CurrentGameMode as DrivingMode).ToggleRepairMode();
			}
		}
	}

	public void OnInventory(InputAction.CallbackContext context)
	{
		InventoryManager.Instance.DisplayInventory(context.ReadValue<float>() == 1.0f);
	}

	public void OnPickUpItem(InputAction.CallbackContext context)
	{
		if (context.ReadValue<float>() == 1.0f &&
			context.phase == InputActionPhase.Performed)
		{
			SpawnManager.Instance.PickupNearbyObjects();
		}
	}

	private void Awake()
	{
		_input = new PlayerAction.PlayerActions(new PlayerAction());
		_input.SetCallbacks(this);
	}

	private void OnEnable()
	{
		_input.Enable();
	}

	private void OnDisable()
	{
		_input.Disable();
	}

	private void OnDestroy()
	{
		_input.Disable();
	}

	private void Update()
	{
		if (GameManager.Instance.CurrentGameMode is DrivingMode)
		{
			DrivingMode drivingMode = GameManager.Instance.CurrentGameMode as DrivingMode;

			if (drivingMode.Car != null)
			{
				drivingMode.Car.SetMovement(_accelerateValue - _brakeValue);
			}
		}
	}
}
