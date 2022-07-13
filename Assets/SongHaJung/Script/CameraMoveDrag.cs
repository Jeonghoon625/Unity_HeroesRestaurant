using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveDrag : MonoBehaviour
{

    Vector3 hitPosition = Vector3.zero;
    Vector3 currentPosition = Vector3.zero;
    Vector3 cameraPosition = Vector3.zero;
    float z = 0.0f;

    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hitPosition = Input.mousePosition;
            cameraPosition = transform.position;
        }
        if (Input.GetMouseButton(0))
        {
            currentPosition = Input.mousePosition;
            LeftMouseDrag();
        }
    }

    void LeftMouseDrag()
    {
        // From the Unity3D docs: "The z position is in world units from the camera."  In my case I'm using the y-axis as height
        // with my camera facing back down the y-axis.  You can ignore this when the camera is orthograhic.
        currentPosition.z = hitPosition.z = cameraPosition.y;

        // Get direction of movement.  (Note: Don't normalize, the magnitude of change is going to be Vector3.Distance(current_position-hit_position)
        // anyways.  
        Vector3 direction = Camera.main.ScreenToWorldPoint(currentPosition) - Camera.main.ScreenToWorldPoint(hitPosition);

        // Invert direction to that terrain appears to move with the mouse.
        direction = direction * -1;

        Vector3 position = cameraPosition + direction;

        transform.position = position;
    }
}

