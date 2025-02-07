using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Function to load the game scene
    public void StartGame()
    {
        Debug.Log("Game Starting...");
        SceneManager.LoadScene("GameScene"); // Make sure "GameScene" exists
    }

    // Function to exit the game
    public void ExitGame()
    {
        Debug.Log("Game Exiting...");
        Application.Quit();
    }
}
