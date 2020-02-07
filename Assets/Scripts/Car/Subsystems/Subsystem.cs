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
        { DamageType.Minor,         5f },
        { DamageType.Intermediate,  10f },
        { DamageType.Major,         20f },
    };

	public static readonly float MAX_DURABILITY = 100f;

    [SerializeField]
    protected SpriteRenderer _renderer = null;

    [SerializeField]
    private SubsystemData _data = null;
    public SubsystemData Data { get { return _data; } }

    [SerializeField]
    [Tooltip("Delay between two damages (in seconds)")]
    private float _delayBetweenDamages = 1.0f;
    private bool _canTakeDamage = true;
    private float _timer = 0f;

	protected float _durability = MAX_DURABILITY;
    public float DurabilityValue { get { return _durability / MAX_DURABILITY; } }
    public bool IsBroken { get {return _durability <= 0f; } }

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

    public void ApplyDamage(DamageType type)
    {
        if (_canTakeDamage && DurabilityByDamage.ContainsKey(type))
        {
            if (_durability > 0f)
            {
                Debug.Log("Apply " + type + " damage (" + DurabilityByDamage[type] + ") to " + (transform.parent != null ? transform.parent.name : gameObject.name));
                _durability -= DurabilityByDamage[type];
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float mag = collision.relativeVelocity.magnitude;

        foreach(KeyValuePair<Pair<float, float>, DamageType> item in DamageTypeByMagnitude)
        {
            if (mag >= item.Key.First && mag < item.Key.Second)
            {
                ApplyDamage(item.Value);
                break;
            }
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

    protected virtual void OnBreak() {}
}