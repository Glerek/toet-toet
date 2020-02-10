using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class SubsystemUI : MonoBehaviour
{
	[SerializeField]
	private Image _icon = null;

	[SerializeField]
	private Gauge _durabilityGauge = null;

	[SerializeField]
	private UILineRenderer _targetLine = null;

	private Subsystem _subsystem = null;
	public Subsystem Subsystem { get { return _subsystem; } }

	private SubsystemContainer _container = null;

	public void Initialize(Subsystem subsystem, SubsystemContainer container)
	{
		_subsystem = subsystem;
		_container = container;
		_icon.sprite = GameManager.Instance.IconData.Icons.Find(item => item.Type == subsystem.Data.Type).Icon;

		Vector3 screenPosition = Camera.main.WorldToScreenPoint(subsystem.transform.position);
		// Vector3 relativeLinePosition = _targetLine.transform.InverseTransformPoint(subsystem.transform.position);
		Vector2 screenPositionUI;
		// RectTransformUtility.WorldToScreenPoint(_container.GetComponent<RectTransform>(), _icon.transform.position, Camera.main, out screenPositionUI);
		screenPositionUI = RectTransformUtility.WorldToScreenPoint(Camera.main, _icon.transform.position);
		Debug.Log($"screenPosition: {screenPosition}");
		// Debug.Log($"relativeLinePosition: {relativeLinePosition}");
		Debug.Log($"screenPositionUI: {screenPositionUI}");
	}

	private void Update()
	{
		if (_subsystem != null)
		{
			_durabilityGauge.Display(_subsystem.DurabilityValue);
		}
	}
}