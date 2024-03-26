using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Block : MonoBehaviour
{
	public GameObject[] groundLines = new GameObject[5];
	public GameObject bottomSoil = null;
	public GameObject middleSoil = null;
	public GameObject topSoil = null;
	public BoxCollider creatingTrigger = null;
	public GameObject obstacleArea = null;
	public Obstacle obstaclePrefab = null;

	public int maxObstacleCount = 3; 
	
	void Start()
	{
		creatingTrigger.isTrigger = true;
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	internal int GenerateBlock()
	{
		bottomSoil = GameController.instance.bottomSoil;
		middleSoil = GameController.instance.middleSoil;
		topSoil = GameController.instance.topSoil;
		int lines = Random.Range(GameController.instance.linesInGame - 1, GameController.instance.linesInGame + 2);
		
		if (lines < 1)
			lines = 1;
		else if (lines > 5)
			lines = 5;

		bool topLift = lines > GameController.instance.linesInGame;
		bool sameLines = lines == GameController.instance.linesInGame;

		//TODO: fix it
		/*if (topLift)
			GameObject.FindGameObjectWithTag("Wall").transform.position += new Vector3(0, 100, 0);
		else if (!sameLines)
			GameObject.FindGameObjectWithTag("Wall").transform.position += new Vector3(0, -100, 0);*/
		
		return lines;
	}

	internal void CreateObstacle()
	{
		// Get area
		if (obstacleArea)
		{
			int obstacleCount = Random.Range(0, maxObstacleCount + 1);
			// Create Obstacle GameObject
			Obstacle obstacle = obstaclePrefab.GetComponent<Obstacle>();
			if (obstacle)
			{
				for (int i = 0; i < obstacleCount; i++)
				{
					// TODO: fix position
					Instantiate(obstacle, new Vector3(obstacleArea.transform.position.x, 0, 0), Quaternion.identity,
								GameObject.FindGameObjectsWithTag("Block").Last().transform);
					// Choose random obstacle by switch (1 or 2) and set data
					int obstacleType = Random.Range(0, 2);
					switch (obstacleType)
					{
						case 0:
							obstacle.type = Obstacle.obstacleType.spike;
							obstacle.damage = 25;
							obstacle.speedDecrement = 20;
							break;
						case 1:
							obstacle.type = Obstacle.obstacleType.slime;
							obstacle.damage = 0;
							obstacle.speedDecrement = 40;
							break;
						default: break;
					}
					// Set sprite to Obstacle
					obstacle.obstacleImage.sprite = obstacle.sprites[obstacleType];
				}
			}
		}
	}
}
