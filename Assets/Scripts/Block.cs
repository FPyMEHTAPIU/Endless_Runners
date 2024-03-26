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
		
		return lines;
	}

	internal void CreateObstacle()
	{
		// Get area
		if (obstacleArea)
		{
			int obstacleCount = Random.Range(0, maxObstacleCount + 1);
			int[] positions = CalculateObstaclesPosition(obstacleCount);
			// Create Obstacle GameObject
			Obstacle obstacle = obstaclePrefab.GetComponent<Obstacle>();
			if (obstacle)
			{
				for (int i = 0; i < obstacleCount; i++)
				{
					Instantiate(obstacle, 
						new Vector3(GameObject.FindGameObjectsWithTag("TopBlock").Last().transform.position.x + positions[i],
									GameObject.FindGameObjectsWithTag("TopBlock").Last().transform.position.y + 100, 0), 
						Quaternion.identity,
						GameObject.FindGameObjectsWithTag("TopBlock").Last().transform);

					// Choose random obstacle by switch (0 or 1) and set data
					int obstacleType = Random.Range(0, 2);
					switch (obstacleType)
					{
						case 0:
							obstacle.type = Obstacle.obstacleType.spike;
							obstacle.damage = 25;
							obstacle.speedDecrement = 60;
							break;
						case 1:
							obstacle.type = Obstacle.obstacleType.slime;
							obstacle.damage = 0;
							obstacle.speedDecrement = 120;
							break;
						default: break;
					}
					// Set sprite to Obstacle
					obstacle.obstacleImage.sprite = obstacle.sprites[obstacleType];
				}
			}
		}
	}

	// TODO: add calculation object, where obstacles must have space between each other at the same block (around 300)
	private int[] CalculateObstaclesPosition(int obstacleTotalNumber)
	{
		int[] positions = new int[obstacleTotalNumber]; 
		for (int i = 0; i < obstacleTotalNumber; i++)
		{
			positions[i] = Random.Range(100, 1820);

			// FIX IT!
			/*if (i > 0 && !((positions[i] - positions[i - 1] < 300) || 
				(positions[i] - positions[i - 1] > 300)))
			{
				if (positions[i - 1] < 400)
				{
					positions[i] += 300;
				}
				else if (positions[i - 1] > 1520)
				{
					positions[i] -= 300;
				}
				else
				{
					int choose = Random.Range(0, 2);
					if (choose == 0)
						positions[i] -= 300;
					else
						positions[i] += 300;
				}
			}*/
		}
		return positions;
	}
}
