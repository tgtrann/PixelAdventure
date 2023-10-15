using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*** THIS SCRIPT CONTROLS THE MOVEMENT OF ANY NON PLAYER CHARACTERS/OBJECTS  ***/
public class WayPointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private SpriteRenderer enemySprite;
    private int currentWayPointIndex = 0;

    [SerializeField] private float speed = 2f;

    // Update is called once per frame
    void Update()
    {
        //if the distance between the two objects is less than 0, perform the following
        if (Vector2.Distance(waypoints[currentWayPointIndex].transform.position, transform.position) < .1f)
        {
            if(this.gameObject.CompareTag("Enemy"))
            {
                enemySprite.flipX = true;
            }
            currentWayPointIndex++;
            //if the currentWayPointIndex reaches 2 or more (the length of the waypoints array) than reset currentWayPointIndex back to 0
            if(currentWayPointIndex >= waypoints.Length)
            {
                if (this.gameObject.CompareTag("Enemy"))
                {
                    enemySprite.flipX = false;
                }
                currentWayPointIndex = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWayPointIndex].transform.position, Time.deltaTime * speed);
    }
}
