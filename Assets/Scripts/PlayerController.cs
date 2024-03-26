using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
	private Rigidbody rb;
	private Player player;
	public bool onGround = false;
	public bool canJump = false;
	public bool isJump = false;
	public bool byWall = false;

	[Range(0f, 1000.0f)]
	public float jumpForce = 250.0f;
	public float fallMultiplier = 60f;
	public float jumpMultiplier = 200.0f;

	private Animator animator = null;
	private int collisionCount = 0;

	// Start is called before the first frame update
	private void Awake()
	{
		player = GetComponent<Player>();
		rb = player.GetComponent<Rigidbody>();
		rb.velocity = new Vector3(player.speed / 10, Physics.gravity.y * (fallMultiplier - 1));
		animator = player.GetComponent<Animator>();
	}

	private void Update()
	{
		// TODO: fix speed
		
		if (onGround)
			rb.velocity = new Vector3(player.speed / 10, 0);
		else
		{
			//rb.velocity += new Vector3(0, Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime);
			rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
		}
			

		if (Input.GetButtonDown("Jump") && onGround)
		{
			onGround = false;
			
			Jump();
		}
		Debug.LogWarning(rb.velocity);
	}

	private void OnCollisionEnter(Collision collision)
	{
		Debug.Log("collider is " + collision.collider.tag);
		if (collision.collider.CompareTag("TopBlock"))
		{
			collisionCount++;
			onGround = true;
			isJump = false;
			rb.velocity = new Vector3(player.speed / 10, 0);
			
			animator.CrossFade("PlayerRun", 0);
		}

		if (collision.collider.CompareTag("Wall") && onGround)
		{
			byWall = true;
			//rb.velocity -= new Vector3(100, 0);
		}
	}

	private void OnCollisionStay(Collision collision)
	{
		/*if (collision.collider.CompareTag("TopBlock"))
		{
			onGround = true;
		}*/
		canJump = false;
		if (collision.collider.CompareTag("Wall") && onGround)
		{
			byWall = true;
			//rb.velocity -= new Vector3(10, 0);
		}
	}

	private void OnCollisionExit(Collision collision)
	{
		if (collision.collider.CompareTag("TopBlock"))
		{
			// set onGround in exit AFTER set in enter or create different logic
			collisionCount--;
			if (collisionCount <= 0)
				onGround = false;
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

	private void FixedUpdate()
	{
		
	}

	// FIX THIS JUMP!!!
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
		Debug.LogWarning(rb.velocity);
	}

	private void OnTriggerEnter(Collider other)
	{
		// TODO: Create hit and slow animation
		if (other.gameObject.CompareTag("Damage"))
		{
			animator.CrossFade("Hit", 0);
			player.health -= other.gameObject.GetComponent<Obstacle>().damage;
		}
	}
}
