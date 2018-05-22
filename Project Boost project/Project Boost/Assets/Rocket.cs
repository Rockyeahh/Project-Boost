using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

    // TODO: Fix the lighting bug.

    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;


    Rigidbody rigidBody;
    AudioSource audioSource;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {
        Rotate();
        Thrust();
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                // do nothing
                break;
            case "Finish":
                // do nothing
                print("Finish");
                SceneManager.LoadScene(1); // Loads scene index 1.
                break;
            default:
                print ("Dead");
                SceneManager.LoadScene(0);
                break;
        }
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * mainThrust); // up uses the y axis.
            if (!audioSource.isPlaying) // So that it doesn't keep playing the same clip on top of eachother.
            {
                audioSource.Play();
            }
        } else
        {
            audioSource.Stop();
        }
    }

    private void Rotate()
    {
        rigidBody.freezeRotation = true; // take manual control of rotation (ignores physics collisions)

        float rotationThisFrame = rcsThrust * Time.deltaTime; // rotationThisFrame based on the thrust.

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame); // forward is a misleading naming. It is not forward, it is a positive z axis rotating the object around that axis by a number/value of 1.
                                               // Think of the z axis turning right or left as a gear and connected to it by a pole is the object moving in a x axis.
                                               // I hate the way that Unity uses axis and how it names things like forward.
                                               // You want to rotate something in the z axis? Well don't use the z axis for that!
                                               // CAN ALSO use transform.Rotate(new Vector3(0f, 0f, 1f)); instead of of .forward
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame); // You can also use .back
        }

        rigidBody.freezeRotation = false; // resume physics engine control of rotation
    }
}
