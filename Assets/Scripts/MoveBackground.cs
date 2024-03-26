using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    public Player player = null;
    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
		transform.position += new Vector3(-player.speed / 2, 0, 0) * Time.deltaTime;
	}
}
