using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameFinishUI : MonoBehaviour
{
	[SerializeField]
	private CanvasGroup _canvasGroup = null;

	[SerializeField]
	private GameObject _stopVHSPanel = null;

	[SerializeField]
	private float _fadeOutTimer = 1.5f;

	[SerializeField]
	private float _fadeInTimer = 1f;

	[SerializeField]
	private CanvasGroup _victoryContainer = null;

	[SerializeField]
	private CanvasGroup _defeatContainer = null;

	private Action _onGameFinishDone = null;

	private void Start()
	{
		_stopVHSPanel.SetActive(false);
		_victoryContainer.alpha = 0;
		_defeatContainer.alpha = 0;
		_canvasGroup.alpha = 0;
	}

	public void Play(bool victory, Action onGameFinishDone)
	{
		_onGameFinishDone = onGameFinishDone;

		StartCoroutine(StartAnimation(victory ? _victoryContainer : _defeatContainer));
	}

	private IEnumerator StartAnimation(CanvasGroup objectToDisplay)
	{
		Camera.main.gameObject.AddComponent<ApplyVHSEffect>();
		_stopVHSPanel.SetActive(true);

		float timer = 0f;
		while (timer < _fadeInTimer)
		{
			objectToDisplay.alpha = Mathf.Lerp(0, 1, timer / _fadeInTimer);

			timer += Time.deltaTime;
			yield return null;
		}

		yield return new WaitForSeconds(2f);

		timer = 0f;
		while (timer < _fadeOutTimer)
		{
			_canvasGroup.alpha = Mathf.Lerp(0, 1, timer / _fadeOutTimer);

			timer += Time.deltaTime;
			yield return null;
		}

		_onGameFinishDone?.Invoke();
	}
}
