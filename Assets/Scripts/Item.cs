using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
	public int value = 1;
	public bool isFlask = false;
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

	// Update is called once per frame
	void Update()
	{

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
			}
			else
			{
				if (type == itemType.coin)
				{
					player.coins += value;
				}
				else
				{
					player.keys += value;
				}
			}
			Destroy(gameObject);
		}
	}
}
