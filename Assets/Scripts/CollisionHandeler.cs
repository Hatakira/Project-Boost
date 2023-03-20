using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandeler : MonoBehaviour
{
    //  PARAMETERS
    [SerializeField] float levelLoadDelay = .5f;
    [SerializeField] AudioClip levelSuccess;
    [SerializeField] AudioClip Crashed;

    //  CACHE
    AudioSource audioSource;

    //  STATE
    bool isTransitioning = false;

    // Start is called before the first frame update
    void Start()
    {
        // CACHING into the variable
        audioSource = GetComponent<AudioSource>();
    }

    // What happens when you collide with an object
    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning) { return; }

        switch (other.gameObject.tag)
        {
            // Friendly Objects
            case "Friendly":
                Debug.Log("You're touching a Friendly");
                break;

            // End of Level Objective
            case "Finish":
                StartSuccessSequence();
                break;

            // Extra Options like Fuel (currently not developed) 
            case "Fuel":
                Debug.Log("You touched the Fuel");
                break;

            // What happens whenever you collide with anything else
            default:
                StartCrashSequence();
                break;
        }
    }

    // Called whenever you sucessfully complete the level with delay
    void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(levelSuccess);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    // Called whenever you crash with delay
    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(Crashed);
        // todo particle effect on crash
        GetComponent<Movement>().enabled = false; 
        Invoke("ReloadLevel", levelLoadDelay);
    }

    // Called for loading the next level in the progression
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    // Called for a reset in levels whenever you crash
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

}

