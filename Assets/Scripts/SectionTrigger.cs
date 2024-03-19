using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SectionTrigger : MonoBehaviour
{
	public GameObject block = null;
	public GameObject backgroundImage = null;

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("3D");
		Debug.Log(other.tag);
		Debug.Log(this.tag);
		if (other.gameObject.CompareTag("NewBlock") && this.tag == "Player")
		{
			// create new ground block inside Canvas
			Instantiate(block, new Vector3(1920, 0, 0), Quaternion.identity, 
				GameObject.FindAnyObjectByType<Canvas>().transform);
			// create new background movable image as child to background Image
			Instantiate(backgroundImage, new Vector3(2000, 0, 0), Quaternion.identity, 
				GameObject.FindGameObjectWithTag("Background").transform);
		}
		else if (other.gameObject.CompareTag("DeleteTrigger") && (this.tag == "Block" || this.tag == "Midground"))
			{
				Debug.Log("DELETING");
				if (this.tag == "Block")
				{
					Debug.Log("- block!");
					Debug.Log(this.name);
					Destroy(block);
				}
				else if (this.tag == "Midground")
				{
					Debug.Log("- image!");
					Debug.Log(this.name);
					//Debug.Log(this.tag);
					Destroy(backgroundImage);
				}
			}
	}
}
