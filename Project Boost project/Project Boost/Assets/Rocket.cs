using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

    // TODO: Fix the lighting bug.

    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] AudioClip mainEngine; // inspector reference box.
    [SerializeField] AudioClip Death;
    [SerializeField] AudioClip Success;

    Rigidbody rigidBody;
    AudioSource audioSource;

    enum State {Alive, Dying, Transcending}; // Three states that set whether we are moving to a new level/scene or not.
    State state = State.Alive; // Default is alive because the ship/player is alive at the start of the level.

    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {
        // TODO: Stop the sound from continuing when state set to Dying.
        if (state == State.Alive)
        {
            RespondToRotateInput();
            RespondToThrustInput();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive)
        {
            return; // return just means end or go no further in this method.
        }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                // don't die
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartDeathSequence();
                break;
        }
    }

    private void StartSuccessSequence()
    {
        state = State.Transcending;
        audioSource.Stop();
        audioSource.PlayOneShot(Success);
        Invoke("LoadNextLevel", 2f); // 1f is basically 1 seconds. // parameterise time = Ben puts this here but what does it mean?
    }

    private void StartDeathSequence()
    {
        state = State.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(Death);
        Invoke("LoadFirstLevel", 3f); // parameterise time = Ben puts this here but what does it mean?
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(1); // Loads scene index 1. TODO: Allow for more than two levels.
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void RespondToThrustInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }
        else
        {
            audioSource.Stop();
        }
    }

    private void ApplyThrust()
    {
        rigidBody.AddRelativeForce(Vector3.up * mainThrust); // up uses the y axis.
        if (!audioSource.isPlaying && state == State.Alive) // So that it doesn't keep playing the same clip on top of eachother.
        {
            audioSource.PlayOneShot(mainEngine);
        }
    }

    private void RespondToRotateInput()
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
