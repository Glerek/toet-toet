using UnityEngine;
using UnityEngine.UI;

public class SubsystemUI : MonoBehaviour
{
	[SerializeField]
	private Image _icon = null;

	[SerializeField]
	private Gauge _durabilityGauge = null;

	private Subsystem _subsystem = null;
	public Subsystem Subsystem { get { return _subsystem; } }

	public void Initialize(Subsystem subsystem)
	{
		_subsystem = subsystem;
		_icon.sprite = (GameManager.Instance.CurrentGameMode as DrivingMode).IconData.Icons.Find(item => item.Type == subsystem.Data.Type).Icon;
	}

	private void Update()
	{
		if (_subsystem != null)
		{
			_durabilityGauge.Display(_subsystem.DurabilityValue);
		}
	}
}