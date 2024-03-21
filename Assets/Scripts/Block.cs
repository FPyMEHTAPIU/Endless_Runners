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
		bool topLift = lines > GameController.instance.linesInGame;
		bool sameLines = lines == GameController.instance.linesInGame;
		
		if (lines < 1)
			lines = 1;
		else if (lines > 5)
			lines = 5;

		/*if (lines == 1)
		{
			groundLines[4] = topSoil;
			groundLines[4].gameObject.SetActive(true);
			for (int i = 3; i >= 0; i--)
			{
				groundLines[i].gameObject.SetActive(false);
			}	
				
		}	
		else if (lines > 1 && lines < 3)
		{
			groundLines[4] = bottomSoil;
			groundLines[3] = topSoil;
			groundLines[4].gameObject.SetActive(true);
				
			groundLines[3].gameObject.SetActive(true);
			for (int i = 2; i >= 0; i--)
			{
				groundLines[i].gameObject.SetActive(false);
			}
		}
		else
		{
			for (int i = 4; i >= 4 - (lines - 1); i--)
			{
				if (i == 4)
					groundLines[i] = bottomSoil;
				else if (i == 4 - (lines - 1))
				{
					groundLines[i] = topSoil;
				}
				else
					groundLines[i] = middleSoil;
			}
			for (int i = 4 - lines - 1; i >= 0; i--)
			{
				groundLines[i].gameObject.SetActive(false);
			}
		}*/
		return lines;
	}
}
