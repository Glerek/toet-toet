using UnityEngine;
using System.Collections;

public class DampCamera2D : MonoBehaviour
{
	public Transform _target = null;
	public Vector2 _delta = Vector2.zero;
	public float _smoothTime = 0.3f;

	private Vector2 _velocity = Vector2.zero;
	private float _zDepth = 0f;

	private void Start()
	{
		if (_target == null)
		{
			_target = (GameManager.Instance.CurrentGameMode as DrivingMode).Car.transform;
		}

		_zDepth = transform.position.z;
	}

	void Update()
	{
		if (_target != null)
		{
			Vector3 newPosition = Vector2.SmoothDamp(transform.position, _target.position, ref _velocity, _smoothTime);
			newPosition.z = _zDepth;

			transform.position = newPosition;
		}
	}
}