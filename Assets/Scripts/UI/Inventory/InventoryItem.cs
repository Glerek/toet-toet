using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private Image _icon = null;

	private GameObject _draggingPlaceholder = null;

    private void Awake()
    {
        _icon.enabled = false;
    }

    public void SetSprite(Sprite sprite)
    {
        _icon.enabled = sprite != null;
        _icon.sprite = sprite;
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
		}
	}
}
