using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Projectile : MonoBehaviour
{
	public int damage = 25;
	public float speed = 1000;
	public bool fromPlayer = true;

	public AudioSource shootSound = null;
	public AudioSource hitSound = null;

	public Sprite[] sprites = new Sprite[2];
	public Image projectileImage = null;

	// Start is called before the first frame update
	void Start()
	{
		Animator animator = GetComponent<Animator>();
		if (fromPlayer)
		{
			speed = 1000.0f;
			damage = 25;
			projectileImage.sprite = sprites[0];

			if (animator)
				animator.CrossFade("Shuriken", 0);
		}
		else
		{
			speed = -750.0f;
			damage = 50;
			projectileImage.sprite = sprites[1];

			if (animator)
				animator.CrossFade("GreenBullet", 0);
		}
	}

	// Update is called once per frame
	void Update()
	{
		transform.position += new Vector3(speed * Time.deltaTime, 0);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("ArrowsCatcher") || other.CompareTag("DeleteTrigger"))
		{
			Destroy(gameObject);
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.CompareTag("TopBlock"))
		{
			Destroy(gameObject);
		}

		if (collision.collider.CompareTag("Player") && !fromPlayer)
		{
			collision.gameObject.GetComponent<Player>().health -= damage;
			Destroy(gameObject);
		}

		if(collision.collider.CompareTag("Enemy") && fromPlayer)
		{
			collision.gameObject.GetComponent<Enemy>().health -= damage;
			Destroy(gameObject);
		}
	}
}
