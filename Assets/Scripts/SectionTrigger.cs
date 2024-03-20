using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SectionTrigger : MonoBehaviour
{
	public GameObject backgroundImage = null;
	Block block = null;

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("3D");
		Debug.Log(other.tag);
		Debug.Log(this.tag);

		// create new blocks
		if (other.gameObject.CompareTag("NewBlock") && this.CompareTag("Player"))
		{
			// TODO: create randomizer for number of lines and set them in GameController
			/*GameObject newBlock = GameObject.Instantiate(GameController.instance.blockPrefab,
															GameController.instance.canvas.gameObject.transform);*/
			block = GameController.instance.blockPrefab.GetComponent<Block>();
			if (block)
			{
				int lines = block.GenerateBlock();
				/*bool topLift = lines > GameController.instance.linesInGame;
				bool sameLines = lines == GameController.instance.linesInGame;*/
				GameController.instance.linesInGame = lines;
				
				// create new ground block inside Canvas
				Instantiate(block, new Vector3(1920, 0, 0), Quaternion.identity,
					GameObject.FindAnyObjectByType<Canvas>().transform);

				/*if (topLift)
				{
					GameObject.FindGameObjectsWithTag("TopBlock").Last().transform.position += new Vector3(0, 100, 0);
					GameObject.FindGameObjectsWithTag("MidBlock").Last().transform.position += new Vector3(0, 100, 0);
				}
				else if (!sameLines)
				{
					GameObject.FindGameObjectsWithTag("TopBlock").Last().transform.position += new Vector3(0, -100, 0);
					GameObject.FindGameObjectsWithTag("MidBlock").Last().transform.position += new Vector3(0, -100, 0);
				}*/

				MoveBlocks(lines);

				// create new background movable image as child to background Image
				Instantiate(backgroundImage, new Vector3(2000, 0, 0), Quaternion.identity,
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
