using System.Collections;
using System.Collections.Generic;
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
		if (Mathf.Abs(transform.position.y - player.transform.position.y) <= 50 && 
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
			Destroy(gameObject);
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
}
