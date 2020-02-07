using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private Image _icon = null;

	private Action<InventoryItem> _startDragCallback = null;
	private Action<InventoryItem> _stopDragCallback = null;
	private SubsystemData _data = null;
	public SubsystemData Data { get { return _data; } }
	private GameObject _draggingPlaceholder = null;

    private void Awake()
    {
        _icon.enabled = false;
    }

	public void Initialize(Action<InventoryItem> startDragCallback, Action<InventoryItem> stopDragCallback)
	{
		_startDragCallback = startDragCallback;
		_stopDragCallback = stopDragCallback;
	}

    public void SetData(SubsystemData data)
    {
		_data = data;
        _icon.enabled = data != null && data.Icon != null;
        _icon.sprite = _data?.Icon;
    }

    public void OnBeginDrag(PointerEventData data)
	{
		if (_icon.enabled)
		{
			Canvas parentCanvas = ComponentFinder.FindInParents<Canvas>(gameObject);
			if (parentCanvas!= null)
			{
				_draggingPlaceholder = new GameObject("Drag Placeholder");
				_draggingPlaceholder.transform.SetParent(parentCanvas.transform, false);
				_draggingPlaceholder.transform.SetAsLastSibling();

				Image placeHolderImage = _draggingPlaceholder.AddComponent<Image>();
				placeHolderImage.sprite = _icon.sprite;
				_draggingPlaceholder.GetComponent<RectTransform>().sizeDelta = new Vector2(_icon.rectTransform.rect.width, _icon.rectTransform.rect.height);

				SetDraggedPosition(data);

				_icon.enabled = false;

				_startDragCallback?.Invoke(this);
			}
		}
	}

    public void OnDrag(PointerEventData data)
    {
		if (_draggingPlaceholder != null)
		{
			SetDraggedPosition(data);
		}
    }

    private void SetDraggedPosition(PointerEventData data)
    {
		RectTransform draggingPlane = transform as RectTransform;

        if (data.pointerEnter != null && data.pointerEnter.transform as RectTransform != null)
            draggingPlane = data.pointerEnter.transform as RectTransform;

        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(draggingPlane, data.position, data.pressEventCamera, out globalMousePos))
        {
            _draggingPlaceholder.GetComponent<RectTransform>().position = globalMousePos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
	{
		if (_draggingPlaceholder != null)
		{
			GameObject.Destroy(_draggingPlaceholder.gameObject);
			_draggingPlaceholder = null;

			_icon.enabled = true;

			Ray ray = GameManager.Instance.Car.RepairUI.RepairCamera.ScreenPointToRay(eventData.position);
			RaycastHit2D hit2D =  Physics2D.GetRayIntersection(ray, 5f, LayerMask.GetMask(new string[] {"RepairablePart"})); 
			if (hit2D.collider != null)
			{
				RepairablePart repairablePart = hit2D.collider.GetComponent<RepairablePart>();
				if (repairablePart != null)
				{
					repairablePart.DropItem(this);
				}
			}

			_stopDragCallback?.Invoke(this);
		}
	}
}
