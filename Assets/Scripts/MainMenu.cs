using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Security.Cryptography;
using System.IO;
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
	public GameObject warningScreen = null;

	private GameData data;

	public AudioSource intro = null;
	public AudioSource loop = null;
	public AudioSource buyTreasureSound = null;
	public AudioSource openTreasureSound = null;
	public AudioSource buttonClickSound = null;
	public GameObject resetButton = null;

	// Start is called before the first frame update
	void Start()
	{
		intro.Play();
		loop.PlayDelayed(intro.clip.length);
		data = SaveSystem.LoadProgress();
		if (data != null)
		{
			coins = data.coins;
			keys = data.keys;
			score = data.highScore;
			player.bonusPlayer = data.bonusPlayer;
			coinsValue.text = coins.ToString();
			keysValue.text = keys.ToString();
			highScore.text = score.ToString("F0");
		}

		if (!player.treasurePurchased)
		{
			shop.SetActive(true);
			if (coins >= 20)
			{
				shop.GetComponentInChildren<Button>().interactable = true;
			}
			else
			{
				shop.GetComponentInChildren<Button>().interactable = false;
			}	
			treasure.SetActive(false);
		}
		else if (player.treasurePurchased && !player.treasureOpened)
		{
			shop.SetActive(false);
			treasure.SetActive(true);
			if (keys >= 2)
			{
				treasure.GetComponentInChildren<Button>().interactable = true;
			}
			else
			{
				treasure.GetComponentInChildren<Button>().interactable = false;
			}
		}
		
		if (player.bonusPlayer)
		{
			shop.SetActive(false);
			treasure.SetActive(false);
		}

		if (!File.Exists(Application.persistentDataPath + "/game.data"))
		{
			resetButton.GetComponent<Button>().interactable = false;
		}
		else
		{
			resetButton.GetComponent<Button>().interactable = true;
		}
	}

	public void PlayGame()
	{
		buttonClickSound.Play();
		SceneManager.LoadScene(1);
	}

	public void BuyTreasure()
	{
		if (shop && coins >= 20)
		{
			buttonClickSound.Play();
			buyTreasureSound.Play();
			coins -= 20;
			coinsValue.text = coins.ToString();
			player.treasurePurchased = true;
			SaveProgress();
			shop.SetActive(false);
			treasure.SetActive(true);
			if (keys >= 2)
			{
				treasure.GetComponentInChildren<Button>().interactable = true;
			}
			else
			{
				treasure.GetComponentInChildren<Button>().interactable = false;
			}
		}
	}

	public void OpenTreasure()
	{
		if (treasure && keys >= 2)
		{
			buttonClickSound.Play();
			openTreasureSound.Play();
			Animator animator = treasure.GetComponent<Animator>();
			if (animator)
			{
				animator.CrossFade("OpenChest", 0);
			}	
			keys -= 2;
			keysValue.text = keys.ToString();
			player.treasureOpened = true;
			player.bonusPlayer = true;
			SaveProgress();
			StartCoroutine(OpenChest());
		}
	}

	private IEnumerator OpenChest()
	{
		yield return new WaitForSeconds(1f);
		treasure.SetActive(false);
		bonusScreen.SetActive(true);
	}

	public void CloseScreen()
	{
		buttonClickSound.Play();
		bonusScreen.SetActive(false);
	}

	public void SaveProgress()
	{
		player.totalCoins = coins;
		player.totalKeys = keys;
		player.maxScore = score;
		SaveSystem.SaveProgress(player);
	}

	public void OpenWarningScreen()
	{
		buttonClickSound.Play();
		warningScreen.SetActive(true);
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
		shop.GetComponentInChildren<Button>().interactable = false;
		treasure.SetActive(false);
		player.treasurePurchased = false;
		player.treasureOpened = false;
		player.bonusPlayer = false;
		SaveSystem.SaveProgress(player);
	}

	public void Yes()
	{
		buttonClickSound.Play();
		ResetProgress();
		warningScreen.SetActive(false);
	}

	public void No()
	{
		buttonClickSound.Play();
		warningScreen.SetActive(false);
	}

	public void Quit()
	{
		buttonClickSound.Play();
		#if UNITY_EDITOR
			EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif
	}
}
