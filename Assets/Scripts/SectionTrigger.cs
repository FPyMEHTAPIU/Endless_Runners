using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
				GameController.instance.CreateBlock(block, other, backgroundImage);
				timerOn = true;
				timer = 3.0f;
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
}
