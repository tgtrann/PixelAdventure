using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*** this script loads the menu scene for the game ***/
public class Menu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Level 1");
    }

}
