using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
	static public PlayerController instance;
	private Rigidbody rb;
	private Player player;
	public bool onGround = false;
	public bool canJump = false;
	public bool isJump = false;
	public bool byWall = false;
	public bool timerOn = false;
	public bool canShoot = true;
	public bool playerHit = false;

	[Range(0f, 1000.0f)]
	public float jumpForce = 250.0f;
	public float fallMultiplier = 60f;
	public float jumpMultiplier = 200.0f;
	public float shootTimer = 0.0f;
	public float timer = 0.0f;
	public GameObject mainMonster = null;
	public MainMonster monster;

	internal Animator animator = null;
	private int collisionCount = 0;

	private void Awake()
	{
		instance = this;
		player = GetComponent<Player>();
		rb = player.GetComponent<Rigidbody>();
		rb.velocity = new Vector3(player.speed / 8, Physics.gravity.y * (fallMultiplier - 1));
		animator = player.GetComponent<Animator>();

		monster = mainMonster.GetComponent<MainMonster>();
		if (mainMonster && monster)
		{
			CreateMonster();
		}
	}

	private void Update()
	{
		//Debug.LogWarning(player.transform.position);
		if (onGround && !timerOn)
		{
			rb.velocity = new Vector3(player.speed / 8, 0);
		}
		else
		{
			rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
		}

		if (Input.GetButtonDown("Jump") && onGround)
		{
			onGround = false;
			Jump();
		}

		if (timerOn)
		{
			timer -= Time.deltaTime;
			if (timer <= 0)
			{
				rb.velocity = Vector3.right * player.speed / 8;
				animator.CrossFade("PlayerRun", 0);
				playerHit = false;
				timerOn = false;
			}
		}

		if (Input.GetButtonDown("Fire1") && canShoot)
		{
			Shoot();
		}

		if (!canShoot)
		{
			shootTimer -= Time.deltaTime;
			if (shootTimer <= 0)
			{
				canShoot = true;
			}
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.CompareTag("TopBlock"))
		{
			collisionCount++;
			onGround = true;
			isJump = false;
			if (!timerOn && !playerHit)
			{
				rb.velocity = new Vector3(player.speed / 8, 0);
			}
			
			animator.CrossFade("PlayerRun", 0);
		}

		if (collision.collider.CompareTag("Wall") && onGround)
		{
			byWall = true;
			rb.velocity = Vector3.right * player.speed / 10;
		}

		if (collision.collider.CompareTag("Monster"))
		{
			player.health = 0;
		}
	}

	private void OnCollisionStay(Collision collision)
	{
		canJump = false;
		if (collision.collider.CompareTag("Wall") && onGround)
		{
			byWall = true;
		}
	}

	private void OnCollisionExit(Collision collision)
	{
		if (collision.collider.CompareTag("TopBlock"))
		{
			// set onGround in exit AFTER set in enter
			collisionCount--;
			if (collisionCount <= 0)
			{
				onGround = false;
			}
			canJump = false;
		}
		if (rb.velocity.y < 0 && !isJump && !onGround)
		{
			animator.CrossFade("Fall", 0);
		}
		if (collision.collider.CompareTag("Wall"))
		{
			byWall = false;
		}
	}

	private void Jump()
	{
		isJump = true;
		if (animator)
		{
			animator.CrossFade("Jump", 0);
		}
		else
		{
			Debug.LogError("No animator found!");
		}
		rb.AddForce((Vector3.up * jumpForce * jumpMultiplier), ForceMode.Impulse);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Damage"))
		{
			timer = 5.0f;
			playerHit = true;
			switch (other.gameObject.GetComponent<Obstacle>().type)
			{
				case Obstacle.obstacleType.spike:
					animator.CrossFade("PlayerHit", 0);
					player.health -= other.gameObject.GetComponent<Obstacle>().damage;
					rb.velocity -= Vector3.right * other.gameObject.GetComponent<Obstacle>().speedDecrement;
					break;
				case Obstacle.obstacleType.slime:
					animator.CrossFade("SlowRun", 0);
					rb.velocity -= Vector3.right * other.gameObject.GetComponent<Obstacle>().speedDecrement;
					break;
				default: break;
			}
			player.healthBar.SetHealth(player.health);
			timerOn = true;
		}
	}

	private void Shoot()
	{
		shootTimer = 0.5f;
		GameObject arrow = player.projectilePrefab;
		if (arrow)
		{
			arrow.GetComponent<Projectile>().fromPlayer = true;
			Instantiate(arrow, player.projectileSpawnPoint.position, Quaternion.identity,
					GameObject.FindAnyObjectByType<Canvas>().transform);
		}
		canShoot = false;
	}

	internal void CreateMonster()
	{
		Instantiate(monster, new Vector3(monster.maxPosition, player.transform.position.y, 0),
											Quaternion.identity, FindAnyObjectByType<Canvas>().transform);
	}
}
