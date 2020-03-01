using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DrivingUI : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI _score = null;

	[SerializeField]
	private GameObject _bestScoreStamp = null;

	private DrivingMode _drivingMode = null;
	private int _bestScore = 0;

	private void Start()
	{
		_drivingMode = GameManager.Instance.CurrentGameMode as DrivingMode;
		
		_bestScore = PlayerPrefs.GetInt("BEST_SCORE", 0);
		_bestScoreStamp.SetActive(false);
	}

	private void Update()
	{
		if (_drivingMode != null)
		{
			int score = _drivingMode.Score;
			_score.text = "SCORE " + ((int)Mathf.Floor(score / 100)).ToString("00") + ":" + (score % 100).ToString("00");

			_bestScoreStamp.SetActive(score > _bestScore);
		}
	}
}
