using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _container = null;

    [SerializeField]
    private Camera _repairCamera = null;

    private void Start()
    {
        OnRepairMode(false);

        GameManager.Instance.Car.OnRepairMode += OnRepairMode;
    }
    
    private void OnDestroy()
    {
        GameManager.Instance.Car.OnRepairMode -= OnRepairMode;
    }

    private void OnRepairMode(bool show)
    {
        _container.SetActive(show);
    }
}
