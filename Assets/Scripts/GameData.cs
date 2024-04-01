using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
	public int coins;
	public float score;
	public float highScore;
	public int keys;

	public bool bonusPlayer;
	public bool treasurePurchased;
	public bool treasureOpened;

	public GameData(Player player)
	{
		coins = player.totalCoins;
		score = player.score;
		highScore = player.maxScore;
		keys = player.totalKeys;
		treasurePurchased = player.treasurePurchased;
		treasureOpened = player.treasureOpened;
		bonusPlayer = player.bonusPlayer;
		if (bonusPlayer)
		{
			player.playerImage.sprite = player.sprites[1];
			player.monster.monsterImage.sprite = player.monster.sprites[1];
		}
		else
		{
			player.playerImage.sprite = player.sprites[0];
			player.monster.monsterImage.sprite = player.monster.sprites[0];
		}
	}
}
