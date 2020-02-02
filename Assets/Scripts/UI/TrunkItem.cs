using UnityEngine;
using UnityEngine.UI;

public class TrunkItem : MonoBehaviour
{
	[SerializeField]
	private Image _icon = null;
	public Image Icon { get { return _icon; } }
}