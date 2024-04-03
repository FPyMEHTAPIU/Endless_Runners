using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
	public int value = 1;
	public bool isFlask = false;

	public UIHandler uiVar;

	public AudioSource heal = null;
	public AudioSource coinKey = null;
	public enum itemType
	{
		healthFlask,
		largeHealthFlask,
		coin,
		key
	}

	public itemType type;
	public Sprite[] sprites = new Sprite[4];
	public Image itemImage = null;

	private Player player;

	// Start is called before the first frame update
	void Start()
	{
		uiVar = FindAnyObjectByType<UIHandler>();
		player = FindAnyObjectByType<Player>();
		Animator animator = GetComponent<Animator>();
		if (animator)
		{
			switch (type)
			{
				case itemType.coin:
					animator.CrossFade("Coin", 0);
					break;
				case itemType.key:
					animator.CrossFade("Key", 0);
					break;
				case itemType.healthFlask:
					animator.CrossFade("Flask", 0);
					break;
				case itemType.largeHealthFlask:
					animator.CrossFade("BigFlask", 0);
					break;
				default: break;
			}
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.CompareTag("Player"))
		{
			if (isFlask)
			{
				player.health += value;
				if (player.health > player.maxHealth)
				{
					player.health = player.maxHealth;
				}
				player.healthBar.SetHealth(player.health);
			}
			else
			{
				if (type == itemType.coin)
				{
					player.coins += value;
					uiVar.SetCoinText(player.coins);
				}
				else
				{
					player.keys += value;
					uiVar.SetKeyText(player.keys);
				}
			}
			StartCoroutine(PlaySound());
		}
	}

	private IEnumerator PlaySound()
	{
		if (isFlask)
		{
			heal.Play();
			gameObject.GetComponentInChildren<Image>().enabled = false;
			yield return new WaitForSeconds(heal.clip.length);
		}
		else
		{
			coinKey.Play();
			gameObject.GetComponentInChildren<Image>().enabled = false;
			yield return new WaitForSeconds(coinKey.clip.length);
		}
		Destroy(gameObject);
	}
}
