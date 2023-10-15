using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

/*** SCRIPT CONTROLS THE CHARACTERS LIFE ***/
public class PlayerLife : MonoBehaviour
{
    Animator anim;
    [SerializeField] private Transform player;
    [SerializeField] private SpriteRenderer playerSprite;

    [SerializeField] private AudioSource deathSoundEffect;
    private AudioSource gameOverSoundEffect;
    
    [Header("Health System")]
    [SerializeField] private int playerHealth;
    [SerializeField] private List <GameObject> hearts;
    private int damageCounter = 1;
    private bool playerTookDamage = false;

    private Vector2 m_startPos;

    // initialize necessary components
    private void Start()
    {
        anim = GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();
        m_startPos = player.transform.position;
        
    }
    
    //check if player took damage and subtract life accordingly
    private void Update()
    {
        if (playerTookDamage)
        {
            // re-set the length
            int heartsLength = hearts.Count - 1;
            Destroy(hearts[heartsLength]);
            hearts.RemoveAt(heartsLength);
            playerTookDamage = false;
        } 
    }

    //allows player to respawn on death (only if player has not depleted health to 0)
    private void Respawn()
    {
        playerSprite.enabled = true;
        transform.position = m_startPos;
        anim.SetInteger("state", 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Trap") || collision.gameObject.CompareTag("Enemy"))
        {
            Die();
            deathSoundEffect.Play();
            playerHealth -= damageCounter;
            playerTookDamage = true;

            if (playerHealth > 0)
            {
                Respawn();
            }
            else
            {
                RestartLevel();
            }
        }
    }

    //player death animation
    private void Die()
    {
        //execute animation for death
        anim.SetTrigger("death");
    }

    //restart level if player health depletes to 0
    private void RestartLevel()
    {
        if (playerHealth <= 0)
        {
            SceneManager.LoadScene("Game Over");
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

}
