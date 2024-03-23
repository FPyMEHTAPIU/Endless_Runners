using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
	private Rigidbody rb;
	private Player player;
	public bool onGround = false;
	public bool canJump = false;

	[Range(0f, 100.0f)]
	public float jumpForce = 25.0f;
	public float jump = 200.0f;
	public float fallMultiplier = 2.5f;
	public float jumpMultiplier = 2.0f;

	private Animator animator = null;

	// Start is called before the first frame update
	private void Awake()
	{
		player = GetComponent<Player>();
		rb = GetComponent<Rigidbody>();
		rb.velocity = new Vector3(player.speed / 10, Physics.gravity.y * (fallMultiplier - 1));
		animator = player.GetComponent<Animator>();
	}

	private void Update()
	{
		// TODO: fix speed
		if (!onGround && rb.velocity.y < 0)
			rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
		else
			rb.velocity = new Vector3(player.speed / 10, 0);
		if (Input.GetButtonDown("Jump") && onGround)
		{
			canJump = true;
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		Debug.Log("collider is " + collision.collider.tag);
		if (collision.collider.CompareTag("TopBlock"))
		{
			onGround = true;
			rb.velocity = new Vector3(player.speed / 7, 0);
			animator.CrossFade("PlayerRun", 0);

			// TODO: set idle animation when player collides to wall
			if (rb.velocity.x <= 0)
			{
				animator.CrossFade("Idle", 0);
			}
		}	
	}

	private void OnCollisionStay(Collision collision)
	{
		if (collision.collider.CompareTag("TopBlock"))
			onGround = true;
	}

	private void OnCollisionExit(Collision collision)
	{
		if (collision.collider.CompareTag("TopBlock"))
		{
			onGround = false;
			canJump = false;
		}
	}

	private void FixedUpdate()
	{
		if (canJump && onGround)
		{
			//rb.AddForce(new Vector3(rb.velocity.x * jumpForce, jump * jumpForce, 0), ForceMode.Impulse);
			Jump(animator);
			//rb.velocity = new Vector3(player.speed, jumpForce, 0);
			onGround = false;
			canJump = false;
		}
	}

	private void Jump(Animator animator)
	{
		if (animator)
		{
			animator.CrossFade("Jump", 0);
		}
		else
		{
			Debug.LogError("No animator found!");
		}
		rb.velocity = new Vector3(0, jump * jumpForce, 0);
		rb.velocity += Vector3.up * Physics.gravity.y * (jumpMultiplier - 1) * Time.deltaTime;
	}	
}
