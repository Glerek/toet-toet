using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>, PlayerAction.IPlayerActions
{
    private PlayerAction.PlayerActions _input;
    private bool _acceleratePushed = false;
    private bool _brakePushed = false;

    public void OnAccelerate(InputAction.CallbackContext context)
    {
        _acceleratePushed = context.ReadValue<float>() == 1.0f;
    }

    public void OnBrake(InputAction.CallbackContext context)
    {
        _brakePushed = context.ReadValue<float>() == 1.0f;
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
        if (_acceleratePushed)
        {
            GameManager.Instance.Car.Accelerate();
        }

        if (_brakePushed)
        {
            GameManager.Instance.Car.Brake();
        }
    }
}
