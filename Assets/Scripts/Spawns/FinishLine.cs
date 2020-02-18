using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
	[SerializeField]
	private bool _victory = false;

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (string.Equals(collider.tag, "Car"))
		{
			(GameManager.Instance.CurrentGameMode as DrivingMode).FinishDrivingMode(_victory);
		}
	}
}
