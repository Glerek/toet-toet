using UnityEngine;

public abstract class IGameMode : MonoBehaviour
{
	[SerializeField]
	private bool _autoStartGameMode = false;

	private void Awake()
	{
		if (_autoStartGameMode)
		{
			StartGameMode();
		}
	}

	private void OnDestroy()
	{
		StopGameMode();
	}

	public abstract void StartGameMode();
	public abstract void StopGameMode();
}