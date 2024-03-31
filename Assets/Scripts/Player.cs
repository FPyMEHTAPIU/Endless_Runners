using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	public int health = 100;
	public int maxHealth = 100;
	public float speed = 400.0f;

	public int coins = 0;
	public int keys = 0;
	public int totalCoins = 0;
	public int totalKeys = 0;
	public Transform projectileSpawnPoint = null;
	public GameObject projectilePrefab = null;

	public Sprite[] sprites = new Sprite[2];
	public Image playerImage = null;

	public HealthBar healthBar = null;

	internal float score = 0;

	// Start is called before the first frame update
	void Start()
	{
		healthBar.SetMaxHealth(maxHealth);
		LoadProgress();
	}

	// Update is called once per frame
	void Update()
	{
		if (health <= 0)
		{
			totalCoins += coins;
			totalKeys += keys;
			SaveProgress();
			Destroy(gameObject);
			// GOTO gameover screen
			Quit();
		}	
	}

	public void SaveProgress()
	{
		SaveSystem.SaveProgress(this);
	}

	public void LoadProgress()
	{
		GameData data = SaveSystem.LoadProgress();
		if (data != null)
		{
			playerImage.sprite = sprites[data.playerSprite];
			totalCoins += data.coins;
			totalKeys += data.keys;
		}
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
