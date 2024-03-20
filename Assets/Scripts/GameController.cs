using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	static public GameController instance = null;
	public int linesInGame = 3;
	public GameObject bottomSoil = null;
	public GameObject middleSoil = null;
	public GameObject topSoil = null;
	public BoxCollider creatingTrigger = null;
	public GameObject blockPrefab = null;
	public Canvas canvas = null;

	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	private void Awake()
	{
		instance = this;
	}
}
