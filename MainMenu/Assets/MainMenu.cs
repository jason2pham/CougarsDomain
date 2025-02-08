using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene"); // Replace "GameScene" with your actual game scene name
    }

    public void ExitGame()
    {
        Debug.Log("Game Quit!"); // For testing in editor
        Application.Quit(); // Only works in a built application
    }
}
