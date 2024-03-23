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
		Debug.Log("3D");
		Debug.Log(other.tag);
		Debug.Log(this.tag);

		// create new blocks
		if (other.gameObject.CompareTag("NewBlock") && this.CompareTag("Player"))
		{
			// TODO: create randomizer for number of lines and set them in GameController
			
			Block newBlock = block.GetComponent<Block>();
			if (newBlock)
			{
				int lines = newBlock.GenerateBlock();
				
				GameController.instance.linesInGame = lines;
				
				// create new ground block inside Canvas
				Instantiate(newBlock, new Vector3(other.transform.position.x + 960, 0, 0), Quaternion.identity,
					GameObject.FindAnyObjectByType<Canvas>().transform);

				MoveBlocks(lines);

				// create new background movable image as child to background Image
				Instantiate(backgroundImage, new Vector3(other.transform.position.x + 1000, 0, 0), Quaternion.identity,
					GameObject.FindGameObjectWithTag("Background").transform);
			}
		}
		// Delete old blocks
		else if (other.gameObject.CompareTag("DeleteTrigger") && (this.CompareTag("Block") || this.CompareTag("Midground")))
		{
			Debug.Log("DELETING");
			if (this.CompareTag("Block"))
			{
				Debug.Log("- block!");
				Debug.Log(this.name);
				Destroy(block);

			}
			else if (this.CompareTag("Midground"))
			{
				Debug.Log("- image!");
				Debug.Log(this.name);
				Destroy(backgroundImage);
			}
		}
	}

	private void MoveBlocks(int lines)
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
