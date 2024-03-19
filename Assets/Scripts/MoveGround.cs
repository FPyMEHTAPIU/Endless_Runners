using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveGround : MonoBehaviour
{
    public Player player = null;

    // Start is called before the first frame update
    void Start()
    {
        //player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(player.speed, 0, 0) * Time.deltaTime;
    }
}
