using System;
using UnityEngine;

public abstract class Pickable : MonoBehaviour
{
	public static readonly float MAX_DURABILITY = 100f;
	protected float _durability = MAX_DURABILITY;
	public float Durability { get { return _durability; } }

	[SerializeField]
	private SpriteRenderer _spriteRenderer = null;
	public SpriteRenderer SpriteRenderer
	{
		get { return _spriteRenderer; }
	}

	[SerializeField]
	private Sprite _icon = null;
	public Sprite Icon
	{
		get { return _icon; }
	}

	private Action<Pickable> _onBreakAction = null;
	public event Action<Pickable> OnBreakAction
	{
		add 
		{
			_onBreakAction -= value;
			_onBreakAction += value;
		}

		remove { _onBreakAction -= value; }
	}

    public void DecreaseDurability(float diff)
    {
        if (_durability > 0.0f)
        {
            _durability -= diff;
            if (_durability <= 0.0f)
            {
                _durability = 0.0f;
				Debug.Log(gameObject.name + " just broke!");

				if (_onBreakAction != null)
				{
					_onBreakAction(this);
				}

                OnBroken();
            }
        }
    }

    public bool CanWork()
    {
        return _durability > 0.0f;
    }

    protected abstract void OnBroken();
}