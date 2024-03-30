using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMonster : MonoBehaviour
{
	private Player player;

	public float monsterPosition;
	public float minPosition = -150.0f;
	public float maxPosition = 150.0f;
	// Start is called before the first frame update
	void Start()
	{
		player = FindAnyObjectByType<Player>();
	}

	// Update is called once per frame
	void Update()
	{
		if (player.GetComponent<Rigidbody>().velocity.x == 50 && monsterPosition > minPosition)
		{
			monsterPosition -= 50.0f * Time.deltaTime;
		}
		transform.position = new Vector3(monsterPosition, player.transform.position.y + 100.0f);
		if (monsterPosition <= minPosition)
		{
			player.monsterAlive = false;
			Destroy(gameObject);
		}
	}
}
