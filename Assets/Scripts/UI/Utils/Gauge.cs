using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Gauge : MonoBehaviour
{
	private Image _gauge = null;

	private void Start()
	{
		_gauge = GetComponent<Image>();
	}

	public void Display(float value)
	{
		_gauge.fillAmount = value; 
	}
}