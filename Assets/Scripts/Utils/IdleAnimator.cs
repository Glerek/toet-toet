using UnityEngine;

public class IdleAnimator : MonoBehaviour
{
	[SerializeField]
	private GameObject _graphics = null;

	[SerializeField]
	private float _speed = 1f;

	private Vector3 _initialScale;

	private void Start()
	{
		_initialScale = _graphics.transform.localScale;
	}

	private void Update()
	{
		_graphics.transform.localScale = new Vector3(_initialScale.x + Mathf.PingPong(Time.time * _speed, 0.2f), _initialScale.y + Mathf.PingPong(Time.time * _speed, 0.1f), 1);;
	}
}