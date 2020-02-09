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

	public void Initialize(Subsystem subsystem)
	{
		_subsystem = subsystem;
		_icon.sprite = GameManager.Instance.IconData.Icons.Find(item => item.Type == subsystem.Data.Type).Icon;

		Vector3 screenPosition = Camera.main.WorldToScreenPoint(subsystem.transform.position);
		Vector3 relativeLinePosition = _targetLine.transform.InverseTransformPoint(screenPosition);
		Debug.Log($"screenPosition: {screenPosition} relativeLinePosition: {relativeLinePosition}");
	}

	private void Update()
	{
		if (_subsystem != null)
		{
			_durabilityGauge.Display(_subsystem.DurabilityValue);
		}
	}
}