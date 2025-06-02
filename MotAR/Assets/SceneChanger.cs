using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene loading

public class SceneChanger : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit(); // Quits the game (only works in a built version)
    }
}
