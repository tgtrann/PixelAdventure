using UnityEngine;
using UnityEngine.SceneManagement;

/*** THIS SCRIPT CONTROLS WHAT HAPPENS WHEN THE LEVEL OR GAME IS COMPLETE ***/
public class LevelComplete : MonoBehaviour
{
    private AudioSource completeLevelSoundEffect;

    // Start is called before the first frame update
    private void Start()
    {
        completeLevelSoundEffect = GetComponent<AudioSource>();
    }
    
    //check if the triggered object is the player and end the level
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            completeLevelSoundEffect.Play();
            Invoke("CompleteLevel", 1.5f);
        }
    }

    //once a level is complete, move to the next scene
    private void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


}
