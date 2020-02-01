using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : Singleton<GameManager>, PlayerAction.IPlayerActions
{
	public GameObject _spawnTarget = null; 
    public Car carPrefab;
    public GameObject cam;
    public GameObject background;
    public GameObject foreground;

    PlayerAction.PlayerActions input;

    private Car car;
	public Car Car { get { return car; } }

    bool pushedAccel = false;

    void Awake()
    {
        input = new PlayerAction.PlayerActions(new PlayerAction());
        input.SetCallbacks(this);

        car = Instantiate(carPrefab, _spawnTarget.transform.position, Quaternion.identity);
    }

    void OnEnable() => input.Enable();
    void OnDestroy() => input.Disable();

    void OnDisable() => input.Disable();

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

    private void updateXPos(Transform t, float x)
    {
        t.position = new Vector3(x, t.position.y, t.position.z);
    }
}
