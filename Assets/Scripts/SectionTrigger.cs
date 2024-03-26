using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class SectionTrigger : MonoBehaviour
{
	public GameObject backgroundImage = null;
	public GameObject block = null;

	private void OnTriggerEnter(Collider other)
	{
		// create new blocks
		if (other.gameObject.CompareTag("NewBlock") && this.CompareTag("Player"))
		{			
			Block newBlock = block.GetComponent<Block>();
			if (newBlock)
			{
				int lines = newBlock.GenerateBlock();
				
				GameController.instance.linesInGame = lines;
				
				// create new ground block inside Canvas
				Instantiate(newBlock, new Vector3(other.transform.position.x + 960, 0, 0), Quaternion.identity,
					GameObject.FindAnyObjectByType<Canvas>().transform);

 				MoveBlocks(lines, newBlock);
				newBlock.CreateObstacle();

				// create new background movable image as child to background Image
				Instantiate(backgroundImage, new Vector3(other.transform.position.x + 1000, 0, 0), Quaternion.identity,
					GameObject.FindGameObjectWithTag("Background").transform);
			}
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

	// Moving block depends on number of lines
	private void MoveBlocks(int lines, Block newBlock)
	{
		switch(lines)
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
