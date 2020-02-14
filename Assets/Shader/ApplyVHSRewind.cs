using UnityEngine;
using System.Collections;

public class ApplyVHSRewind : MonoBehaviour
{
	public float SpeedX = 0f;
	public float SpeedY = 0f;

	private Material _material;

	void Awake()
	{
		_material = new Material(Shader.Find("Custom/VHSRewind"));
		_material.SetTexture("_SecondaryTex", Resources.Load("Textures/TVnoiseRewind") as Texture);
		_material.SetFloat("_OffsetDistortion", 480f);
		_material.SetFloat("_SpeedX", SpeedX);
		_material.SetFloat("_SpeedY", SpeedY);
	}

	public void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		float offsetSpeedX = _material.GetFloat("_OffsetSpeedX");
		if (offsetSpeedX > 0.0f)
		{
			_material.SetFloat("_OffsetSpeedX", offsetSpeedX - Random.Range(0f, offsetSpeedX));
		}
		else if (offsetSpeedX < 0.0f)
		{
			_material.SetFloat("_OffsetSpeedX", offsetSpeedX + Random.Range(0f, -offsetSpeedX));
		}
		else if (Random.Range(0, 15) == 1)
		{
			_material.SetFloat("_OffsetSpeedX", Random.Range(-10f, 10f));
		}

		float offsetSpeedY = _material.GetFloat("_OffsetSpeedY");
		if (offsetSpeedY > 0.0f)
		{
			_material.SetFloat("_OffsetSpeedY", offsetSpeedY - Random.Range(0f, offsetSpeedY));
		}
		else if (offsetSpeedY < 0.0f)
		{
			_material.SetFloat("_OffsetSpeedY", offsetSpeedY + Random.Range(0f, -offsetSpeedY));
		}
		else if (Random.Range(0, 15) == 1)
		{
			_material.SetFloat("_OffsetSpeedY", Random.Range(-1f, 1f));
		}

		if (Random.Range(0, 15) == 1)
		{
			_material.SetFloat("_OffsetDistortion", Random.Range(1f, 480f));
		}
		else
		{
			_material.SetFloat("_OffsetDistortion", 480f);
		}

		Graphics.Blit(source, destination, _material);
	}
}