using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : IGameMode
{
	[SerializeField]
	private ApplyVHSEffect _vhsPlayEffect = null;
	
	[SerializeField]
	private ApplyVHSRewind _vhsRewindEffect = null;

	[SerializeField]
	private AudioSource _rewindSounds = null;

	[SerializeField]
	private GameObject _rewindImage = null;

	[SerializeField]
	private GameObject _rewindText = null;

	[SerializeField]
	private CanvasGroup _canvasGroup = null;
	
	[SerializeField]
	private float _fadeDuration = 1.5f;
	
	[SerializeField]
	private RawImage _renderTexture = null;

	private bool _duringRewind = false;

	public override void StartGameMode()
	{
		_rewindImage.SetActive(false);
		_rewindText.SetActive(true);

		_vhsPlayEffect.enabled = true;
		_vhsRewindEffect.enabled = false;
	}

	public override void StopGameMode()
	{
		SceneManager.UnloadSceneAsync("GameOver");
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.R) && !_duringRewind)
		{
			StartCoroutine(StartRewind());
		}
	}

	private IEnumerator StartRewind()
	{
		_duringRewind = true;

		_rewindImage.SetActive(true);
		_rewindText.SetActive(false);

		_vhsPlayEffect.enabled = false;
		_vhsRewindEffect.enabled = true;

		_rewindSounds.Play();

		AsyncOperation asyncLoad = GameManager.Instance.StartGameMode(GameManager.GameMode.DrivingMode);
		asyncLoad.allowSceneActivation = false;

		while (!asyncLoad.isDone)
		{
			if (asyncLoad.progress >= 0.9f && !_rewindSounds.isPlaying)
			{
				float timer = 0f;

				while (timer < _fadeDuration)
				{
					timer += Time.deltaTime;
					_canvasGroup.alpha = Mathf.Lerp(1, 0, timer / _fadeDuration);
					yield return null;
				}

				asyncLoad.allowSceneActivation = true;

				timer = 0f;
				Color renderTextureColor = _renderTexture.color;

				while (timer < _fadeDuration)
				{
					timer += Time.deltaTime;
					renderTextureColor.a = Mathf.Lerp(1, 0, timer / _fadeDuration);
					_renderTexture.color = renderTextureColor;
					yield return null;
				}
			}

			yield return null;
		}

		_duringRewind = false;
	}
}
