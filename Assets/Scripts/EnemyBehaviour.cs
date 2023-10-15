using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*** THIS SCRIPT CONTROLS THE ENEMY BEHAVIOUR (redundant script, check waypoint follower ***/
public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWayPointIndex = 0;
    private SpriteRenderer sprite;

    [SerializeField] private float speed = 2f;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        //if the distance between the two objects is less than 0, perform the following
        if (Vector2.Distance(waypoints[currentWayPointIndex].transform.position, transform.position) < .1f)
        {
            sprite.flipX = true;
            currentWayPointIndex++;
            //if the currentWayPointIndex reaches 2 or more (the length of the waypoints array) than reset currentWayPointIndex back to 0
            if (currentWayPointIndex >= waypoints.Length)
            {
                sprite.flipX = false;
                currentWayPointIndex = 0;
            }
        }

        //have the game object move towards the waypoint positions
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWayPointIndex].transform.position, Time.deltaTime * speed);
    }
}


