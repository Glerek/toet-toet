using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreen : IGameMode
{
	[SerializeField]
	private string _gameSceneName = string.Empty;

	[SerializeField]
	private CanvasGroup _canvasGroup = null;

	[SerializeField]
	private RawImage _renderTexture = null;

	[SerializeField]
	private float _fadeDuration = 1.5f;

	private bool _duringLoad = false;

	public override void StartGameMode()
	{
	}

	public override void StopGameMode()
	{
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Return) && !_duringLoad)
		{
			StartCoroutine(LoadGameScene());
		}
	}

	private IEnumerator LoadGameScene()
	{
		_duringLoad = true;

		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_gameSceneName, LoadSceneMode.Additive);
		asyncLoad.allowSceneActivation = false;

		while (!asyncLoad.isDone)
		{
			if (asyncLoad.progress >= 0.9f)
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

		SceneManager.UnloadSceneAsync("Start");
		_duringLoad = false;
	}
}