using UnityEngine;

public class Movement : MonoBehaviour
{
    //  PARAMETERS - for tuning, typically set in the editor
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotateThrust = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mParticalThrust


    //  CACHE - e.g. references for readability or speed
    Rigidbody rb;
    AudioSource audioSource;

    //  STATE - private instance (member) variables | ie.. bool isAlive;
    // Currently no states

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    // ProcessThrust is called for Vertical lift on the Y access.
    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
        }

        else
        {
            audioSource.Stop();
        }
    }

    //ProcessRotation is called for Left and Right rotation
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotateThrust);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotateThrust);
        }
    }
    
    // Used to apply collision interferance on rotation 
    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing rotation so the physics system can take over.
    }
}
