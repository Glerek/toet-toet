using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrivingUI : Singleton<DrivingUI>
{
	[SerializeField]
	private Canvas _canvas = null;

	public List<TrunkItem> _inventory = new List<TrunkItem>();
	[SerializeField]
	private SubsystemUI _wheelsSubsystem = null;

	[SerializeField]
	private SubsystemUI _lightsSubsystem = null;

	public HitchhikerUI _hitchhiker = null;

	public void Display(bool show)
	{
		_canvas.gameObject.SetActive(show);
	}

	private void Update()
	{
		_wheelsSubsystem._subsystemHpGauge.Display(GameManager.Instance.Car.WheelsDurability / Pickable.MAX_DURABILITY);
		_lightsSubsystem._subsystemHpGauge.Display(GameManager.Instance.Car.LightsDurability / Pickable.MAX_DURABILITY);
	}
}
