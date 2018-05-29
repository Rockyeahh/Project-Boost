using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent] // Stops you from adding more than one of these to the same gameobject. This stops it from getting confusing.
public class Oscillator : MonoBehaviour {

    [SerializeField] Vector3 movementVector = new Vector3 (10f, 10f, 10f); // What? It seems to set the vector3 axis in code, removing the need for the dsigner to set it. But why?
    [SerializeField] float period = 2f; // Why? 

    float movementFactor; // 0 for not moved, 1 for fully moved. Range and serialize work together to make a slider.
    Vector3 startingPos;

	void Start () {
        startingPos = transform.position; // sets the startingPos because otherwise it wouldn't set itself before update tried to do anything. It's like starting the call without knowing where the peddles are.
	}
	
	void Update () {
        // TODO protect against period is zero.
        if (period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period; // What? If the game time is whatever in seconds then it would be divided by the period of 2f
                                           // then it'd be that in the float cycles.    AND cause it's in update, it grows continually from 0 each frame.

        const float tau = Mathf.PI * 2f; // about 6.28          Also I still don't understand PI.
        float rawSinWave = Mathf.Sin(cycles * tau); // Goes from -1 to +1.

        //print(rawSinWave); // Should print between -1 and +1. I don't know why but he says this is correct. I don't know how and barely what.

        movementFactor = rawSinWave / 2f + 0.5f;
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset; // startingPos vector 3 takes the offset and shoves it ontothe transform.position.
	}
}
