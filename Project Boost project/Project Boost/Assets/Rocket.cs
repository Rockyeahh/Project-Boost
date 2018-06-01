using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float levelLoadDelay = 2f; // levelLoadDelay currently makes the start and death use the same time. If they need to be a diferent a seperate levelLoadDelay would need to be written.

    [SerializeField] AudioClip mainEngine; // inspector reference box.
    [SerializeField] AudioClip Death;
    [SerializeField] AudioClip Success;

    [SerializeField] ParticleSystem mainEngineParticles; // inspector reference box.
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] ParticleSystem successParticles;

    Rigidbody rigidBody;
    AudioSource audioSource;

    enum State {Alive, Dying, Transcending}; // Three states that set whether we are moving to a new level/scene or not.
    State state = State.Alive; // Default is alive because the ship/player is alive at the start of the level.

    bool CollisionsAreEnabled = true;

    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {
        if (state == State.Alive)
        {
            RespondToRotateInput();
            RespondToThrustInput();
        }
        if (Debug.isDebugBuild) // checks if it's the development build.
        {
            RespondToDebugKeys();
        }
    }

    private void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            print("stop collisions");
            CollisionsAreEnabled = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive || !CollisionsAreEnabled)
        {
            return;
        }

        {
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
    }

    private void StartSuccessSequence()
    {
        state = State.Transcending;
        audioSource.Stop();
        audioSource.PlayOneShot(Success);
        successParticles.Play();
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    private void StartDeathSequence()
    {
        state = State.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(Death);
        deathParticles.Play();
        Invoke("LoadFirstLevel", levelLoadDelay);
    }

    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int NextSceneIndex = currentSceneIndex + 1;
        if (NextSceneIndex == SceneManager.sceneCountInBuildSettings) //  staic int number of scenes in the build order.
        {
            NextSceneIndex = 0; // loop back to the first level.
        }
        SceneManager.LoadScene(NextSceneIndex);
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
            mainEngineParticles.Stop();
        }
    }

    private void ApplyThrust()
    {
        rigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime); // up uses the y axis.
        if (!audioSource.isPlaying && state == State.Alive) // So that it doesn't keep playing the same clip on top of eachother.
        {
            audioSource.PlayOneShot(mainEngine);
            mainEngineParticles.Play();
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
