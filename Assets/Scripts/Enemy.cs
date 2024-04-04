using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
	public float speedDecrement = 50.0f;
	public HealthBar healthBar = null;

	public AudioSource shootSound = null;
	public AudioSource attackSound = null;

	private Player player = null;
	private float attackTimer = 0.0f;
	private bool canAttack = true;
	private Animator animator;
	private bool attacked = false;

	// Start is called before the first frame update
	void Start()
	{
		animator = GetComponent<Animator>();
		player = FindAnyObjectByType<Player>();
		if (isMelee)
		{
			enemyImage.sprite = sprites[0];
			health = 50;
			if (animator)
				animator.CrossFade("PlantIdle", 0);
			attacked = false;
		}
		else
		{
			enemyImage.sprite = sprites[1];
			health = 25;
			if (animator)
				animator.CrossFade("Slime", 0);
		}
		healthBar.SetEnemyMaxHealth(health);
	}

	// Update is called once per frame
	void Update()
	{
		if (Mathf.Abs(transform.position.y - player.transform.position.y) <= 25 && 
					transform.position.x - player.transform.position.x > 0 &&
					transform.position.x <= 1960 &&
					!isMelee && canAttack)
		{
			Shoot();
		}

		if (transform.position.x - player.transform.position.x <= 350 && 
			Mathf.Abs(transform.position.y - player.transform.position.y) <= 25 && isMelee && canAttack)
		{
			Attack();
		}

		if (!canAttack)
		{
			attackTimer -= Time.deltaTime;
			if (attackTimer <= 0)
				canAttack = true;
		}

		if (health <= 0)
			Die();
	}

	private void Shoot()
	{
		attackTimer = 2.0f;
		GameObject arrow = projectilePrefab;
		if (arrow)
		{
			arrow.GetComponent<Projectile>().fromPlayer = false;
			Instantiate(arrow, projectileSpawnPoint.position, Quaternion.identity,
					GameObject.FindAnyObjectByType<Canvas>().transform);
			shootSound.Play();
		}
		canAttack = false;
	}

	private void Attack()
	{
		attackTimer = 2.0f;
		if (animator && !attacked)
		{
			animator.CrossFade("PlantAttack", 0);
			attackSound.Play();
			attacked = true;
		}
		canAttack = false;
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.CompareTag("Player"))
		{
			player.health -= damage;
			player.healthBar.SetHealth(player.health);
			player.GetComponent<Rigidbody>().velocity -= Vector3.right * speedDecrement;
			PlayerController.instance.timer = 3.0f;
			PlayerController.instance.timerOn = true;
			PlayerController.instance.playerHit = true;

			if (PlayerController.instance.animator)
			{
				PlayerController.instance.playerHitSound.Play();
				if (!player.bonusPlayer)
				{
					PlayerController.instance.animator.CrossFade("PlayerHit", 0);
				}
				else
				{
					PlayerController.instance.animator.CrossFade("BonusPlayerHit", 0);
				}
			}	
		}
	}

	private void Die()
	{
		player = FindAnyObjectByType<Player>();
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
		Destroy(gameObject);
	}
}
