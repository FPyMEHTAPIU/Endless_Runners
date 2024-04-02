using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class MainMonster : MonoBehaviour
{
	private Player player;

	public float monsterPosition;
	public float minPosition = -150.0f;
	public float maxPosition = 150.0f;
	public bool soundOn = false;

	//internal int spriteNumber = 0;
	public Image monsterImage = null;
	public Sprite[] sprites = new Sprite[2];
	public AudioSource beeSound = null;

	// Start is called before the first frame update
	void Start()
	{
		player = FindAnyObjectByType<Player>();
		LoadProgress();
		Animator animator = GetComponent<Animator>();
		if (animator)
		{
			if (player.bonusPlayer)
			{
				animator.CrossFade("NYAN", 0);
			}
			else
			{
				animator.CrossFade("MainMonster", 0);
			}
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (player.GetComponent<Rigidbody>().velocity.x == player.speed / 8 && monsterPosition > minPosition)
		{
			monsterPosition -= 50.0f * Time.deltaTime;
			if (soundOn && monsterPosition <= 0 && !player.bonusPlayer)
			{
				beeSound.Stop();
				soundOn = false;
			}
		}
		transform.position = new Vector3(monsterPosition, player.transform.position.y + 100.0f);
		if ((player.transform.position.x <= 720 || PlayerController.instance.playerHit) 
			&& monsterPosition < maxPosition)
		{
			monsterPosition += 300.0f * Time.deltaTime;
			if (!soundOn && monsterPosition >= 0 && !player.bonusPlayer)
			{
				beeSound.Play();
				soundOn = true;
			}

		}
		// Set moster as upper layer than blocks
		transform.SetSiblingIndex(9);
	}

	public void LoadProgress()
	{
		GameData data = SaveSystem.LoadProgress();
		if (data != null)
		{
			player.bonusPlayer = data.bonusPlayer;
			/*if (player.bonusPlayer)
			{
				player.playerImage.sprite = player.sprites[1];
				monsterImage.sprite = sprites[1];
			}
			else
			{
				player.playerImage.sprite = player.sprites[0];
				monsterImage.sprite = sprites[0];
			}*/
		}
	}
}
