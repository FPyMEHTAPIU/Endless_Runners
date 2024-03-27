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
			GameController.instance.CreateBlock(block, other, backgroundImage);
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
