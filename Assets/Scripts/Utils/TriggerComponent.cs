using UnityEngine;
using UnityEngine.Events;
using System;

public class TriggerComponent : MonoBehaviour
{
	[Serializable]
	public class Collider2DUnityEvent : UnityEngine.Events.UnityEvent<GameObject, Collider2D> { }

	[SerializeField]
	private Collider2DUnityEvent _triggerEvent = null;

	private void OnTriggerEnter2D(Collider2D collider)
	{
		_triggerEvent?.Invoke(gameObject, collider);
	}
}