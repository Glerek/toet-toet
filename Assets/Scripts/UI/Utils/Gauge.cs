using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Gauge : MonoBehaviour
{
	[SerializeField]
	private Image _gauge = null;

	public void Display(float value)
	{
		_gauge.fillAmount = value; 
	}
}