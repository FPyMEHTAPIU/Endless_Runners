using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
	public bool isMelee = false;
	public int health = 25;
	public int damage = 25;
	public Transform projectileSpawnPoint = null;
	public GameObject projectilePrefab = null;
	public Sprite[] sprites = new Sprite[2];
	public Image enemyImage = null;
	public GameObject item = null;

	private Player player = null;
	private float shootTimer = 0.0f;
	private bool canShoot = true;

	// Start is called before the first frame update
	void Start()
	{
		Animator animator = GetComponent<Animator>();
		player = FindAnyObjectByType<Player>();
		if (isMelee)
		{
			enemyImage.sprite = sprites[0];
			health = 50;
			if (animator)
				animator.CrossFade("PlantIdle", 0);
		}
		else
		{
			enemyImage.sprite = sprites[1];
			health = 25;
			if (animator)
				animator.CrossFade("Slime", 0);
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (Mathf.Abs(transform.position.y - player.transform.position.y) <= 25 && 
					transform.position.x - player.transform.position.x > 0 && !isMelee && canShoot)
		{
			Shoot();
		}

		if (!canShoot)
		{
			shootTimer -= Time.deltaTime;
			if (shootTimer <= 0)
				canShoot = true;
		}

		if (health <= 0)
			Die();
		// TODO:
		// Make Punch for melee enemy
	}

	private void Shoot()
	{
		shootTimer = 2.0f;
		GameObject arrow = projectilePrefab;
		if (arrow)
		{
			arrow.GetComponent<Projectile>().fromPlayer = false;
			Instantiate(arrow, projectileSpawnPoint.position, Quaternion.identity,
					GameObject.FindAnyObjectByType<Canvas>().transform);
		}
		canShoot = false;
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.CompareTag("Player"))
			player.health -= damage;
	}

	private void Die()
	{
		player = FindAnyObjectByType<Player>();
		bool drop = Random.value > 0.5f;
		if (drop)
		{
			// drop object
			Item newItem = item.GetComponent<Item>();
			if (newItem)
			{
				Instantiate(newItem, transform.position, Quaternion.identity, 
							GameObject.FindGameObjectsWithTag("TopBlock").Last().transform);
				int chooseType = Random.Range(0, 10);
				if (chooseType == 3)
				{
					newItem.type = Item.itemType.key;
					newItem.value = 1;
					newItem.isFlask = false;
				}
				else
				{
					if (player.health <= player.maxHealth / 2)
					{
						chooseType = Random.Range(0, 2);
					}
					else if (player.health == player.maxHealth)
					{
						chooseType = 2;
					}
					else
					{
						chooseType = Random.Range(0, 3);
					}
					switch (chooseType)
					{
						case 0:
							newItem.type = Item.itemType.healthFlask;
							newItem.value = 25;
							newItem.isFlask = true;
							break;
						case 1:
							newItem.type = Item.itemType.largeHealthFlask;
							newItem.value = 50;
							newItem.isFlask = true;
							break;
						case 2:
							newItem.type = Item.itemType.coin;
							newItem.value = 1;
							newItem.isFlask = false;
							break;
						default: break;
					}
				}
				newItem.itemImage.sprite = newItem.sprites[chooseType];
			}
		}
		Destroy(gameObject);
	}
}
