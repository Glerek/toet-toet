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

	[SerializeField]
	private GameObject _playText = null;

	[SerializeField]
	private GameObject _playImage = null;

	private bool _duringLoad = false;

	public override void StartGameMode(object data)
	{
		_playText.SetActive(true);
		_playImage.SetActive(false);
	}

	public override void StopGameMode()
	{
		SceneManager.UnloadSceneAsync("Start");
	}

	private void Update()
	{
		if (!_duringLoad &&
			(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
		{
			StartCoroutine(LoadGameScene());
		}
	}

	private IEnumerator LoadGameScene()
	{
		_duringLoad = true;

		_playText.SetActive(false);
		_playImage.SetActive(true);

		AsyncOperation asyncLoad = GameManager.Instance.StartGameMode(GameManager.GameMode.DrivingMode);
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

		_duringLoad = false;
	}
}