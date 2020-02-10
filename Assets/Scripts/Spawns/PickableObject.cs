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

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (string.Equals(col.tag, "Car"))
        {
            SpawnManager.Instance.RegisterPickable(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (string.Equals(collider.tag, "Car"))
        {
            SpawnManager.Instance.UnregisterPickable(this);
        }
    }
}
