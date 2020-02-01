using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : Singleton<CarManager>, PlayerAction.IPlayerActions
{
    public Car carPrefab;

    PlayerAction.PlayerActions input;

    private Car car;
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
        car = Instantiate(carPrefab, new Vector3(0, 0, 0), Quaternion.identity);
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
