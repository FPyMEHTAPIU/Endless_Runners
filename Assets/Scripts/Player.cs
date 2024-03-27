using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;

public class Player : MonoBehaviour
{
	public int health = 100;
	public float speed = 400.0f;

	private int coins = 0;
	public Transform projectileSpawnPoint = null;
	public GameObject projectilePrefab = null;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		/*if (health <= 0)
			Destroy(gameObject);*/
	}
}
