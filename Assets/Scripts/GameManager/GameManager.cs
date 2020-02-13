using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
	public enum GameMode
	{
		Boot = 0,
		TitleScreen,
		DrivingMode,
		GameOver,
	}

	public static readonly Dictionary<GameMode, string> SceneByGameMode = new Dictionary<GameMode, string>()
	{
		{ GameMode.Boot, 			"Boot" },
		{ GameMode.TitleScreen, 	"Start" },
		{ GameMode.DrivingMode, 	"AmosTesting" },
	};

	[SerializeField]
	private GameMode _startingGameMode = GameMode.Boot;

	private IGameMode _currentGameMode = null;
	public IGameMode CurrentGameMode { get { return _currentGameMode; }	}

	private List<IGameMode> _registeredGameModes = new List<IGameMode>();

	private void Start()
	{
		for (int i = 0; i < _registeredGameModes.Count; i++)
		{
			if (_registeredGameModes[i].GameMode == _startingGameMode)
			{
				StartGameMode(_registeredGameModes[i]);
				break;
			}
		}
	}

	public void RegisterGameMode(IGameMode gameMode)
	{
		_registeredGameModes.Add(gameMode);
	}

	public void UnregisterGameMode(IGameMode gameMode)
	{
		_registeredGameModes.Remove(gameMode);
		gameMode.StopGameMode();
	}

	public AsyncOperation StartGameMode(GameMode gameMode)
	{
		if (SceneByGameMode.ContainsKey(gameMode))
		{
			AsyncOperation loadOperation = SceneManager.LoadSceneAsync(SceneByGameMode[gameMode], LoadSceneMode.Additive);

			loadOperation.completed += (AsyncOperation operation) => {
				StartGameMode(_registeredGameModes.Find(item => item.GameMode == gameMode));
			};

			return loadOperation;
		}

		throw new System.Exception("Can't find related Scene name for " + gameMode.ToString());
	}

	private void StartGameMode(IGameMode gameMode)
	{
		gameMode.StartGameMode();
		_currentGameMode = gameMode;
	}
}
