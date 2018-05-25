using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent] // Stops you from adding more than one of these to the same gameobject. This stops it from getting confusing.
public class Oscillator : MonoBehaviour {

    [SerializeField] Vector3 movementVector;
    // Remove from inspector later.
    [Range(0, 1)] [SerializeField] float movementFactor; // 0 for not moved, 1 for fully moved. Range and serialize work together to make a slider.

    Vector3 startingPos;

	// Use this for initialization
	void Start () {
        startingPos = transform.position; // sets the startingPos because otherwise it wouldn't set itself before update tried to do anything. It's like starting the call without knowing where the peddles are.
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset; // startingPos vector 3 takes the offset and shoves it ontothe transform.position.
	}
}
