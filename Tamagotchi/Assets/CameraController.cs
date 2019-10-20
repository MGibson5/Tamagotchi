using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform[] moveCameraTo;
    private int currentTransform;

    private Transform start;

    // Movement speed in units per second.
    [SerializeField]private float speed = 1.0F;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;

    private bool move = false;


    //DEBUG STUFF
    public bool DEBUGMOVECAMERA;
    public int CAMERAPOSITION;

    public void MoveCamera(int _changeBy){

        if (currentTransform + _changeBy < moveCameraTo.Length)
        {
            currentTransform += _changeBy;

            // Keep a note of the time the movement started.
            startTime = Time.time;
            start = transform;

            // Calculate the journey length.
            journeyLength = Vector3.Distance(start.position, moveCameraTo[currentTransform].position);

            move = true;
        }
        else { move = false; }
    }    
    
    // Move to the target end position.
    void Update()
    {
        if(DEBUGMOVECAMERA)
        {
            DEBUGMOVECAMERA = false;
            MoveCamera(CAMERAPOSITION);
        }

        if (move)
        {
            // Distance moved equals elapsed time times speed..
            float distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / journeyLength;

            if (fractionOfJourney >= 1)
            {
                move = false;
            }
            else
            {
                // Set our position as a fraction of the distance between the markers.
                transform.position = Vector3.Lerp(start.position, moveCameraTo[currentTransform].position, fractionOfJourney);
            }
            
        }
    }
}
