using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Security.Cryptography;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
	public Text coinsValue = null;
	public Text keysValue = null;
	public Text highScore = null;

	public Player player = null;
	public MainMonster monster = null;
	public int coins;
	public int keys;
	public float score;

	public GameObject shop = null;
	public GameObject treasure = null;
	public GameObject bonusScreen = null;

	private GameData data;

	// Start is called before the first frame update
	void Start()
	{
		data = SaveSystem.LoadProgress();
		if (data != null)
		{
			coins = data.coins;
			keys = data.keys;
			score = data.highScore;
			coinsValue.text = coins.ToString();
			keysValue.text = keys.ToString();
			highScore.text = score.ToString("F0");
		}

		if (!player.treasurePurchased)
		{
			shop.SetActive(true);
			treasure.SetActive(false);
		}
		else if (player.treasurePurchased && !player.treasureOpened)
		{
			shop.SetActive(false);
			treasure.SetActive(true);
		}
		else
		{
			shop.SetActive(false);
			treasure.SetActive(false);
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

	public void BuyTreasure()
	{
		if (shop && coins >= 20)
		{
			coins -= 20;
			coinsValue.text = coins.ToString();
			player.treasurePurchased = true;
			SaveProgress();
			shop.SetActive(false);
			treasure.SetActive(true);
		}
	}

	public void OpenTreasure()
	{
		if (treasure && keys >= 2)
		{
			Animator animator = treasure.GetComponent<Animator>();
			if (animator)
			{
				animator.CrossFade("OpenChest", 0);
			}	
			keys -= 2;
			keysValue.text = keys.ToString();
			treasure.SetActive(false);
			player.treasureOpened = true;
			player.bonusPlayer = true;
			SaveProgress();
			StartCoroutine(OpenChest());
		}
	}

	private IEnumerator OpenChest()
	{
		yield return new WaitForSeconds(1f);
		bonusScreen.SetActive(true);
	}

	public void CloseScreen()
	{
		bonusScreen.SetActive(false);
	}

	public void SaveProgress()
	{
		player.totalCoins = coins;
		player.totalKeys = keys;
		player.maxScore = score;
		SaveSystem.SaveProgress(player);
	}

	public void ResetProgress()
	{
		player.totalCoins = coins = 0;
		player.totalKeys = keys = 0;
		player.maxScore = data.highScore = score = 0;
		coinsValue.text = coins.ToString();
		keysValue.text = keys.ToString();
		highScore.text = data.highScore.ToString("F0");
		shop.SetActive(true);
		treasure.SetActive(false);
		player.treasurePurchased = false;
		player.treasureOpened = false;
		player.bonusPlayer = false;
		SaveSystem.SaveProgress(player);
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
