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

    // Start is called before the first frame update
    void Start()
    {
        car = Instantiate(carPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        Debug.Log(background.GetComponent<RectTransform>().sizeDelta.x);
    }

    // Update is called once per frame
    void Update()
    {
        if (pushedAccel)
        {
            car.accel();
        }

        float carX = car.transform.position.x;
        updateXPos(cam.transform, carX);
        updateXPos(background.transform, carX);
        updateXPos(foreground.transform, carX);
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
