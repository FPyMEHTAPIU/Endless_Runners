using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;

public class Player : MonoBehaviour
{
    public int health = 100;
    public float speed = 400.0f;
    public float maxSpeed = 500.0f;

    private int coins = 0;
	private GameObject deleteTrigger = null;

	// Start is called before the first frame update
	void Start()
    {

		//rb.velocity += new Vector3(0, -100, 0);
        deleteTrigger = GameObject.FindGameObjectWithTag("DeleteTrigger");
    }

    // Update is called once per frame
    void Update()
    {
		
	}
}
