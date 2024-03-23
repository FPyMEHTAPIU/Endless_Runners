using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveGround : MonoBehaviour
{
    public Player player = null;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: fix speed
        rb.velocity = (new Vector3(-player.speed, 0, 0));
    }
}
