﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boot : IGameMode
{
	public override void StartGameMode()
	{
        GameManager.Instance.StartGameMode(GameManager.GameMode.TitleScreen);
	}

	public override void StopGameMode()
	{
	}
}