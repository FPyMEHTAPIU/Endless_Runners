using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
	public Text coinsValue = null;
	public Text keysValue = null;
	public Text highScore = null;

	//public Player player = null;
	public MainMonster monster = null;

	public bool treasurePurchased = false;

	// public GameObject shop = null;
	// public GameObject treasure = null;

	// Start is called before the first frame update
	void Start()
	{
		GameData data = SaveSystem.LoadProgress();
		if (data != null)
		{
			//player.playerImage.sprite = player.sprites[data.playerSprite];
			monster.monsterImage.sprite = monster.sprites[data.mainMonsterSprite];
			//player.coins = data.coins;
			//player.keys = data.keys;
			coinsValue.text = data.coins.ToString();
			keysValue.text = data.keys.ToString();
			highScore.text = data.highScore.ToString("F0");
		}
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	public void PlayGame()
	{
		SceneManager.LoadScene(1);
	}

	public void Quit()
	{
		#if UNITY_EDITOR
			EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif
	}
}
