using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	public int health = 100;
	public int maxHealth = 100;
	public float speed = 400.0f;

	public int coins = 0;
	public int keys = 0;
	public Transform projectileSpawnPoint = null;
	public GameObject projectilePrefab = null;

	public Sprite[] sprites = new Sprite[2];
	public Image playerImage = null;
	public bool monsterAlive = false;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		/*if (health <= 0)
			Destroy(gameObject);*/
	}

	public void SaveProgress()
	{
		SaveSystem.SaveProgress(this);
	}

	public void LoadProgress()
	{
		GameData data = SaveSystem.LoadProgress();
		playerImage.sprite = sprites[data.playerSprite];
	}
}
