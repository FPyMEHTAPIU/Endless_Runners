using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMonster : MonoBehaviour
{
	private Player player;

	public float monsterPosition;
	public float minPosition = -150.0f;
	public float maxPosition = 150.0f;

	//internal int spriteNumber = 0;
	public Image monsterImage = null;
	public Sprite[] sprites = new Sprite[2];

	// Start is called before the first frame update
	void Start()
	{
		player = FindAnyObjectByType<Player>();
		Animator animator = GetComponent<Animator>();
		if (animator)
		{
			if (player.bonusPlayer)
			{
				animator.CrossFade("NYAN", 0);
			}
			else
			{
				animator.CrossFade("MainMonster", 0);
			}
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (player.GetComponent<Rigidbody>().velocity.x == 50 && monsterPosition > minPosition)
		{
			monsterPosition -= 50.0f * Time.deltaTime;
		}
		transform.position = new Vector3(monsterPosition, player.transform.position.y + 100.0f);
		if ((player.transform.position.x <= 720 || PlayerController.instance.playerHit) 
			&& monsterPosition < maxPosition)
		{
			monsterPosition += 300.0f * Time.deltaTime;
		}
		// Set moster as upper layer than blocks
		transform.SetSiblingIndex(9);
	}
}
