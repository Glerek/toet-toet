using UnityEngine;

public abstract class Pickable : MonoBehaviour
{
	public const float MAX_DURABILITY = 100f;
	protected float _durability = MAX_DURABILITY;

	protected Sprite _icon = null;
	public Sprite Icon
	{
		get { return _icon; }
	}
}