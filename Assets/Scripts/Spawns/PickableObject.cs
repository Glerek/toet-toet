using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PickableObject : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private SpriteRenderer _icon = null;

    private SubsystemData _data = null;
    public SubsystemData Data { get { return _data; } }
    
    public void Initialize(SubsystemData data)
    {
        _data = data;
        _icon.sprite = _data.Icon;
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        SpawnManager.Instance.PickupObject(this);
    }
}
