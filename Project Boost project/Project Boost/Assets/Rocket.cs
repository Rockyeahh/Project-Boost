using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    Rigidbody rigidBody;
    AudioSource audioSource;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

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
            if (!audioSource.isPlaying) // So that it doesn't keep playing the same clip on top of eachother.
            {
            audioSource.Play();
            }
        } else
        {
            audioSource.Stop();
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward); // forward is a misleading naming. It is not forward, it is a positive z axis rotating the object around that axis by a number/value of 1.
                                               // Think of the z axis turning right or left as a gear and connected to it by a pole is the object moving in a x axis.
                                               // I hate the way that Unity uses axis and how it names things like forward.
                                               // You want to rotate something in the z axis? Well don't use the z axis for that!
        } else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward); // You can also use .back
        }
    }
}
