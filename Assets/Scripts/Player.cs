using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;

public class Player : MonoBehaviour
{
    public int health = 100;
    public float speed = -50.0f;
    public float maxSpeed = -100.0f;
    private int coins = 0;
    /*public GameObject block = null;
    public Canvas canvas = null;*/

    // Start is called before the first frame update
    void Start()
    {
		Debug.Log(GetComponent<Collider>());
		Debug.Log(GetComponent<Collider>().tag);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	/*private void OnTriggerEnter(Collider other)
	{
		Debug.Log("3D");
		Debug.Log(other.tag);
		Debug.Log(this.tag);
		if (other.gameObject.CompareTag("NewBlock"))
			Instantiate(block, new Vector3(1770, 0, 0), Quaternion.identity, GameObject.FindAnyObjectByType<Canvas>().transform);
	}*/
}
