using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
	public int score = 0;

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

	internal void CreateBlock(GameObject block, Collider other, GameObject backgroundImage)
	{
		Block newBlock = block.GetComponent<Block>();
		if (newBlock)
		{
			int lines = newBlock.GenerateBlock();

			GameController.instance.linesInGame = lines;

			// create new ground block inside Canvas
			Instantiate(newBlock, new Vector3(other.transform.position.x + 960, 0, 0), Quaternion.identity,
				GameObject.FindAnyObjectByType<Canvas>().transform);

			MoveBlocks(lines);
			bool isObstacles = Random.value > 0.5f;
			if (isObstacles)
				newBlock.CreateObstacle();
			else
				newBlock.SpawnEnemies();

			// create new background movable image as child to background Image
			Instantiate(backgroundImage, new Vector3(other.transform.position.x + 1000, 0, 0), Quaternion.identity,
				GameObject.FindGameObjectWithTag("Background").transform);
		}
	}

	// Moving block depends on number of lines
	private void MoveBlocks(int lines)
	{
		switch (lines)
		{
			case 1:
				GameObject.FindGameObjectsWithTag("TopBlock").Last().transform.position += new Vector3(0, -200, 0);
				GameObject.FindGameObjectsWithTag("MidBlock").Last().transform.position += new Vector3(0, -200, 0);
				break;
			case 2:
				GameObject.FindGameObjectsWithTag("TopBlock").Last().transform.position += new Vector3(0, -100, 0);
				GameObject.FindGameObjectsWithTag("MidBlock").Last().transform.position += new Vector3(0, -100, 0);
				break;
			case 4:
				GameObject.FindGameObjectsWithTag("TopBlock").Last().transform.position += new Vector3(0, 100, 0);
				GameObject.FindGameObjectsWithTag("MidBlock").Last().transform.position += new Vector3(0, 100, 0);
				break;
			case 5:
				GameObject.FindGameObjectsWithTag("TopBlock").Last().transform.position += new Vector3(0, 200, 0);
				GameObject.FindGameObjectsWithTag("MidBlock").Last().transform.position += new Vector3(0, 200, 0);
				break;
			default: break;
		}
	}
}
