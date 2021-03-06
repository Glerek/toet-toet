using UnityEngine;
using System;
using System.Collections.Generic;

public class Subsystem : MonoBehaviour
{
	public enum DamageType
	{
		Minor,
		Intermediate,
		Major,
	}

	private static Dictionary<Pair<float, float>, DamageType> DamageTypeByMagnitude = new Dictionary<Pair<float, float>, DamageType>()
	{
		{ new Pair<float, float>(3f, 6f),                       DamageType.Minor},
		{ new Pair<float, float>(6f, 15f),                      DamageType.Intermediate},
		{ new Pair<float, float>(15f, float.PositiveInfinity),  DamageType.Major},
	};

	private static readonly Dictionary<DamageType, float> DurabilityByDamage = new Dictionary<DamageType, float>()
	{
		{ DamageType.Minor,         2f },
		{ DamageType.Intermediate,  5f },
		{ DamageType.Major,         10f },
	};

	public static readonly float MAX_DURABILITY = 100f;

	[SerializeField]
	protected bool _showDamages = false;

	[SerializeField]
	protected SpriteRenderer _renderer = null;

	[SerializeField]
	private SubsystemData _data = null;
	public SubsystemData Data { get { return _data; } }

	[SerializeField]
	[Tooltip("Delay between two damages (in seconds)")]
	private float _delayBetweenDamages = 1.0f;

	[SerializeField]
	private bool _unlimitedDurability = false;

	[SerializeField]
	private bool _useDamageTypeSystem = false;

	private bool _canTakeDamage = true;
	private bool ShouldApplyDamage { get { return !_unlimitedDurability && _canTakeDamage; } }
	private float _timer = 0f;

	protected float _durability = MAX_DURABILITY;
	public float DurabilityValue { get { return _durability / MAX_DURABILITY; } }
	public bool IsBroken { get { return _durability <= 0f; } }

	private Action<Subsystem> _onBreakAction = null;
	public event Action<Subsystem> OnBreakAction
	{
		add
		{
			_onBreakAction -= value;
			_onBreakAction += value;
		}

		remove { _onBreakAction -= value; }
	}

	public void Initialize(SubsystemData data)
	{
		_data = data;
		_renderer.sprite = _data.Icon;
	}

	public Vector2 GetWorldSize()
	{
		Vector2 size = Vector2.zero;

		if (_renderer.sprite != null)
		{
			size.x = _renderer.sprite.rect.width / _renderer.sprite.pixelsPerUnit;
			size.y = _renderer.sprite.rect.height / _renderer.sprite.pixelsPerUnit;
		}

		return size;
	}

	public void ApplyDamage(DamageType type)
	{
		if (ShouldApplyDamage && DurabilityByDamage.ContainsKey(type))
		{
			if (_durability > 0f)
			{
				if (_showDamages)
				{
					Debug.Log("Apply " + type + " damage to " + (transform.parent != null ? transform.parent.name : gameObject.name));
				}

				DecreaseDurability(DurabilityByDamage[type]);
			}
		}
	}

	private void DecreaseDurability(float damage)
	{
		if (ShouldApplyDamage && _durability > 0f)
		{
			if (_showDamages)
			{
				Debug.Log("Decrease durability from " + _durability + " to " + (_durability - damage) + ") to " + (transform.parent != null ? transform.parent.name : gameObject.name));
			}
			_durability -= damage;
			_canTakeDamage = false;

			if (_durability <= 0f)
			{
				_durability = 0f;

				if (_onBreakAction != null)
				{
					_onBreakAction(this);
				}

				OnBreak();
			}
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		float mag = collision.relativeVelocity.magnitude;

		if (_showDamages)
		{
			Debug.Log($"{gameObject.name} hit {collision.gameObject.name} with magnitude {mag}");
		}

		if (_useDamageTypeSystem)
		{
			foreach (KeyValuePair<Pair<float, float>, DamageType> item in DamageTypeByMagnitude)
			{
				if (mag >= item.Key.First && mag < item.Key.Second)
				{
					ApplyDamage(item.Value);
					break;
				}
			}
		}
		else
		{
			DecreaseDurability(3f * mag);
		}
	}

	private void Update()
	{
		if (!_canTakeDamage)
		{
			_timer += Time.deltaTime;

			if (_timer >= _delayBetweenDamages)
			{
				_canTakeDamage = true;
				_timer = 0f;
			}
		}
	}

	protected virtual void OnBreak() { }
}