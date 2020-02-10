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

    [SerializeField]
    private float _shineWidthMin = 0.05f;
    
    [SerializeField]
    private float _shineWidthMax = 0.1f;

    [SerializeField]
    private float _animationDuration = 2f;

    private Material _material = null;
    private float _timer = 0f;


    public void Initialize(SubsystemData data)
    {
        _data = data;
        _icon.sprite = _data.Icon;
        _material = _icon.material;
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

    private void Update()
    {
        if (_material != null)
        {
            float t = _timer / _animationDuration;
            _material.SetFloat("_ShineLocation", Mathf.Lerp(0, 1, t));

            if (_timer < 0.5f)
            {
                _material.SetFloat("_ShineWidth", Mathf.Lerp(_shineWidthMin, _shineWidthMax, t * 2f));
            }
            else
            {
                _material.SetFloat("_ShineWidth", Mathf.Lerp(_shineWidthMax, _shineWidthMin, (t - 0.5f) * 2f));
            }

            _timer += Time.deltaTime;

            if (_timer > _animationDuration)
            {
                _timer = 0f;
            }
        }
    }
}
