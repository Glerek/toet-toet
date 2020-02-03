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
    private SubsystemData _data = null;
    public SubsystemData Data { get { return _data; } }

	protected float _durability = MAX_DURABILITY;
    public float Durability { get { return _durability; } }
    public bool IsBroken { get {return _durability <= 0f; } }

	private Action<Subsystem> _onBreak = null;
	public event Action<Subsystem> OnBreak
	{
		add 
		{
			_onBreak -= value;
			_onBreak += value;
		}

		remove { _onBreak -= value; }
	}

    public void ApplyDamage(DamageType type)
    {
        if (DurabilityByDamage.ContainsKey(type))
        {
            if (_durability > 0f)
            {
                Debug.Log("Apply " + type + " damage (" + DurabilityByDamage[type] + ") to " + (transform.parent != null ? transform.parent.name : gameObject.name));
                _durability -= DurabilityByDamage[type];

                if (_durability <= 0f)
                {
                    _durability = 0f;

                    if (_onBreak != null)
                    {
                        _onBreak(this);
                    }
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
}