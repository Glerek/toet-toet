using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
	[SerializeField]
	private Image _icon = null;

	private void Awake()
	{
		_icon.enabled = false;
	}

	public void SetSprite(Sprite sprite)
	{
		_icon.enabled = sprite != null;
		_icon.sprite = sprite;
	}
}
