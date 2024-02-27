using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public GameObject lossScreen;
    public GameObject winScreen; // Assuming you have a win screen GameObject

    private bool gameStarted = false;

    void Start()
    {
        PauseGame(); // Pause the game when it starts
        lossScreen.SetActive(false); // Ensure the loss screen is initially deactivated
        winScreen.SetActive(false); // Ensure the win screen is initially deactivated
    }

    void Update()
    {
        if (!gameStarted)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                StartGame();
            }
        }
        else
        {
            // Check for R key press to restart the game
            if (Input.GetKeyDown(KeyCode.R))
            {
                RestartGame();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("house")) // Use "house" instead of "House"
        {
            Debug.Log("Player entered the house trigger!");
            WinGame();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (gameStarted && collision.gameObject.CompareTag("animal"))
        {
            Die();
        }
    }

    void StartGame()
    {
        gameStarted = true;
        ResumeGame(); // Resume the game when it starts
    }

    void PauseGame()
    {
        Time.timeScale = 0f; // Pause the game
    }

    void ResumeGame()
    {
        Time.timeScale = 1f; // Resume the game
    }

    void Die()
    {
        Debug.Log("Player died!");

        // Activate the loss screen
        lossScreen.SetActive(true);

        PauseGame();
    }

    void WinGame()
    {
        Debug.Log("You win!");

        // Activate the win screen
        winScreen.SetActive(true);

        PauseGame();
    }

    public void RestartGame()
    {
        // Reload the current scene to restart the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f; // Ensure time scale is reset to normal
    }
}
