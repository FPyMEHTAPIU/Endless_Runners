using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class SectionTrigger : MonoBehaviour
{
	public GameObject backgroundImage = null;
	public GameObject block = null;

	public float timer = 0.0f;
	public bool timerOn = false;

	private void Update()
	{
		if (timerOn)
		{
			timer -= Time.deltaTime;
			if (timer <= 0)
				timerOn = false;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		// create new blocks
		if (other.gameObject.CompareTag("NewBlock") && this.CompareTag("Player"))
		{
			if (!timerOn)
			{
				// changing first block
				GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");

 				int lines = blocks.Last().GetComponent<Block>().GenerateBlock();
				bool sameLines = lines == GameController.instance.linesInGame;
				GameController.instance.linesInGame = lines;
				
				ImageChanger changer = blocks.Last().GetComponent<Block>().GetComponent<ImageChanger>();
				if (changer)
				{
					bool lastBlock = false;
					changer.ChangeRightBox(sameLines, lastBlock);
				}

				GameController.instance.CreateBlock(block, other, lines, sameLines);
				timerOn = true;
				timer = 3.0f;
			}	
		}
		// create new background
		if (other.gameObject.CompareTag("NewBackground") && this.CompareTag("Player"))
		{
			GameController.instance.CreateBackground(backgroundImage, other);
		}
		// Delete old blocks
		else if (other.gameObject.CompareTag("DeleteTrigger") && 
			(this.CompareTag("Block") || this.CompareTag("Midground")))
		{
			if (this.CompareTag("Block"))
			{
				Destroy(block);
			}
			else if (this.CompareTag("Midground"))
			{
				Destroy(backgroundImage);
			}
		}
	}
}
