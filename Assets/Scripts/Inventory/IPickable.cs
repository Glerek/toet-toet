using UnityEngine;

public abstract class Pickable : MonoBehaviour
{
	public static readonly float MAX_DURABILITY = 100f;
	protected float _durability = MAX_DURABILITY;
	public float Durability { get { return _durability; } }

	protected Sprite _icon = null;
	public Sprite Icon
	{
		get { return _icon; }
	}

    public void DecreaseDurability(float diff)
    {
        if (_durability > 0.0f)
        {
            _durability -= diff;
            if (_durability <= 0.0f)
            {
                _durability = 0.0f;
                OnBroken();
            }
        }
    }

    public bool CanWork()
    {
        return _durability > 0.0f;
    }

    public abstract void OnBroken();
}