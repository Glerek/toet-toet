using UnityEngine;

public abstract class IGameMode : MonoBehaviour
{
	[SerializeField]
	private GameManager.GameMode _gameMode = GameManager.GameMode.Boot;
	public GameManager.GameMode GameMode { get { return _gameMode; } }

	private void Awake()
	{
		GameManager.Instance.RegisterGameMode(this);
	}

	private void OnDestroy()
	{
		if (GameManager.HasInstance)
		{
			GameManager.Instance.UnregisterGameMode(this);
		}
	}

	public abstract void StartGameMode(object data);
	public abstract void StopGameMode();
}