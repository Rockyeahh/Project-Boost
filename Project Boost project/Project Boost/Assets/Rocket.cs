using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();

    }
	
	// Update is called once per frame
	void Update () {
        ProcessInput();
	}

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up); // up uses the y axis.
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward); // forward uses the z axis. By pressing A the ship goes anti clockwise/minus x. It looks like minus x to me. This is angering me.
                                               // I guess that forward and backward are not doing what Ben is saying. He probably hasn't noticed that x is the left and or right not z.
                                               // The Unity docs state that z is the axis of forward but our game doesn't follow that.
                                               // We won't use the z axis in this 2.5D game. He hasn't mentinoed that it is 2.5D but right now it seems to be that.
        } else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward); // You can also use .back
        }
    }
}
