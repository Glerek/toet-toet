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

    // private void Update()
    // {
    //     Ray ray = _repairCamera.ScreenPointToRay(Input.mousePosition);
    //     Debug.DrawRay(ray.origin, 5f * ray.direction, Color.red, 5f);
    //     RaycastHit2D hit2D =  Physics2D.GetRayIntersection(ray, 5f, LayerMask.GetMask(new string[] {"Car", "Wheel"})); 
    //     if (hit2D.collider != null)
    //     {
    //         Debug.Log(hit2D.collider);
    //     }
    // }
}
