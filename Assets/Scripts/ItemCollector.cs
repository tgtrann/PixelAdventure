using UnityEngine;
using UnityEngine.UI;

/** SCRIPT CONTROLS THE ITEMS IN THE GAME **/
public class ItemCollector : MonoBehaviour
{
    private int melonCount = 0;
    [SerializeField] private Text counterText;
    [SerializeField] private AudioSource collectItemSoundEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Melon"))
        {
            Destroy(collision.gameObject);
            melonCount++;
            collectItemSoundEffect.Play();
            if(melonCount >= 0 && melonCount < 10)
            {
                counterText.text = "Melon: " + "0" + melonCount;
            }
            else if (melonCount >= 10)
            {
                counterText.text = "Melon: " + melonCount;
            }
        }
    }
}
