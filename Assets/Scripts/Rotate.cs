using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*** THIS SCRIPT CONTROLS ANY GAME OBJECT ROTATION - THIS WAS PRIMARILY USED FOR THE SAW TRAP ***/
public class Rotate : MonoBehaviour
{
    [SerializeField] private float speed = 2f;

    // Update is called once per frame
    private void Update()
    {
        transform.Rotate(0, 0, 360 * speed * Time.deltaTime);
    }
}
