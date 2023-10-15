using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*** SCRIPT ENSURES THE PLAYER DOESN'T STICK TO THE TERRAIN WALLS ETC. ****/
public class StickyPlatform : MonoBehaviour
{
    //this method allows the gameObject that has collided to become a child of a parent
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if the game object that collided is the player than set the player as a child object of this parent
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }

    //this method allows the collided gameObject to exit from the parent
    private void OnTriggerExit2D(Collider2D collision)
    {
        //if the game object that collided is the player than remove the player from the parent
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}
