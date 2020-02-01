using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrivingUI : MonoBehaviour
{
	public List<TrunkItem> _inventory = new List<TrunkItem>();
	public List<SubsystemUI> _subsystems = new List<SubsystemUI>();
	public HitchhikerUI _hitchhiker = null;

	public void Initialize()
	{
		
	}

	private void Update()
	{
		float averageDurability = 0f;
		for (int i = 0; i < GameManager.Instance.Car.WheelObjects.Length; i++)
		{
			averageDurability += GameManager.Instance.Car.WheelObjects[i].GetComponent<Wheel>().Durability;
			averageDurability /= (i + 1);
		}

		_subsystems[0]._subsystemHpGauge.Display(averageDurability / Pickable.MAX_DURABILITY);

		_subsystems[1]._subsystemHpGauge.Display(GameManager.Instance.Car.LightObject.GetComponent<CarLight>().Durability / Pickable.MAX_DURABILITY);
	}
}
