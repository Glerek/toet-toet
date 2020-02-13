using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _container = null;

    [SerializeField]
    private Camera _repairCamera = null;
	public Camera RepairCamera { get {return _repairCamera; } }

    private void Start()
    {
        OnRepairMode(false);

        (GameManager.Instance.CurrentGameMode as DrivingMode).Car.OnRepairMode += OnRepairMode;
    }
    
    private void OnDestroy()
    {
        if (GameManager.HasInstance)
        {
            (GameManager.Instance.CurrentGameMode as DrivingMode).Car.OnRepairMode -= OnRepairMode;
        }
    }

    private void OnRepairMode(bool show)
    {
        _container.SetActive(show);
    }
}
