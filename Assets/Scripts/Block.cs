using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour
{
	public GameObject[] groundLines = new GameObject[5];
	public GameObject bottomSoil = null;
	public GameObject middleSoil = null;
	public GameObject topSoil = null;
	public BoxCollider creatingTrigger = null;
	public GameObject spawnArea = null;
	public Obstacle obstaclePrefab = null;
	public GameObject enemyPrefab = null;

	public int maxObstacleCount = 3;

	void Start()
	{
		creatingTrigger.isTrigger = true;
	}

	private void Update()
	{
		if (transform.position.x <= -3800)
		{
			Destroy(gameObject);
		}
	}

	internal int GenerateBlock()
	{
		int lines = Random.Range(GameController.instance.linesInGame - 1, GameController.instance.linesInGame + 2);
		
		if (lines < 1)
			lines = 1;
		else if (lines > 5)
			lines = 5;
		
		return lines;
	}

	internal void CreateObstacle()
	{
		int obstacleCount = Random.Range(1, maxObstacleCount + 1);
		// Get area
		if (spawnArea)
		{
			int[] positions = CalculatePosition(obstacleCount);
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
							obstacle.speedDecrement = 70;
							break;
						case 1:
							obstacle.type = Obstacle.obstacleType.slime;
							obstacle.damage = 0;
							obstacle.speedDecrement = 100;
							break;
						default: break;
					}
					// Set sprite to Obstacle
					obstacle.obstacleImage.sprite = obstacle.sprites[obstacleType];
				}
			}
		}
	}

	private int[] CalculatePosition(int obstacleTotalNumber)
	{
		int[] positions = new int[obstacleTotalNumber];
		if (obstacleTotalNumber > 1)
		{
			int positionGap = 2535 / obstacleTotalNumber;
			for (int i = 0; i < obstacleTotalNumber; i++)
			{
				if (i == 0)
					positions[i] = Random.Range(100, positionGap);
				else
					positions[i] = Random.Range(positions[i - 1] + 300, positionGap * (i + 1));
			}
		}
		else if (obstacleTotalNumber == 1)
		{
			positions[0] = Random.Range(100, 2535);
		}
		return positions;
	}

	internal void SpawnEnemies()
	{
		// Choose random number of enemies from 1 to 2
		int enemiesCount = Random.Range(1, 3);

		// Calculating positions for enemies
		int[] positions = CalculatePosition(enemiesCount);

		Enemy enemy = enemyPrefab.GetComponent<Enemy>();
		if (enemy)
		{
			for (int i = 0; i < enemiesCount; i++)
			{
				Instantiate(enemy, 
						new Vector3(GameObject.FindGameObjectsWithTag("TopBlock").Last().transform.position.x + positions[i],
									GameObject.FindGameObjectsWithTag("TopBlock").Last().transform.position.y + 100, 0),
						Quaternion.identity,
						GameObject.FindGameObjectsWithTag("TopBlock").Last().transform);

				// Choose type of enemies (melee or range)
				enemy.isMelee = Random.value < 0.5f;
				if (enemy.isMelee)
					enemy.enemyImage.sprite = enemy.sprites[0];
				else
					enemy.enemyImage.sprite = enemy.sprites[1];
			}
		}
	}
}
