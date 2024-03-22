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
	public bool onGround = true;
	public bool canJump = true;

	public float jumpForce = 1500.0f;
	public float jump;
	
	// Start is called before the first frame update
	private void Awake()
	{
		player = GetComponent<Player>();
		rb = GetComponent<Rigidbody>();
		rb.velocity = new Vector3(player.speed / 4, -100);
		jump = 500;
	}

	private void Update()
	{
		// TODO: fix speed
		if (!onGround)
			rb.velocity = new Vector3(player.speed / 4, -100);
		if (Input.GetButtonDown("Jump") && onGround)
		{
			canJump = true;
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		Debug.Log("collider is " + collision.collider.tag);
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
			rb.velocity = new Vector3(0, (jumpForce * 2), 0);
			onGround = false;
			canJump = false;
		}
	}
}
