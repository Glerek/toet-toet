using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarManager : Singleton<CarManager>, PlayerAction.IPlayerActions
{
    public Car car;

    PlayerAction.PlayerActions input;

    bool pushedAccel = false;

    void Awake()
    {
        input = new PlayerAction.PlayerActions(new PlayerAction());
        input.SetCallbacks(this);
    }

    void OnEnable() => input.Enable();
    void OnDestroy() => input.Disable();

    void OnDisable() => input.Disable();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pushedAccel)
        {
            car.accel();
        }
    }

    public void OnAccel(InputAction.CallbackContext context)
    {
        pushedAccel = context.ReadValue<float>() == 1.0f;
    }
}
