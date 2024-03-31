using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
	public int coins;
	public float highScore;
	public int keys;

	public int playerSprite;
	public int mainMonsterSprite;

	public GameData(Player player)
	{
		coins = player.totalCoins;
		if (highScore < player.score)
			highScore = player.score;
		keys = player.totalKeys;
	}
}
