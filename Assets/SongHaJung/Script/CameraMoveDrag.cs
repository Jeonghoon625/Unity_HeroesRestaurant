using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMoveDrag : MonoBehaviour
{
    public float xmove = 0;
    private float movespeed = 0.01f;
    private float wheelspeed = 0.01f;

    private float limitY = 0.025f;
    private float limitZ = -0.1f;

    private float verticalPosition = 0.45f;

    private Camera cam;

    int minZoom = 20;
    int maxZoom = 36;
    int zoomValue = 25;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }
    void Update()
    {
        if(!EventSystem.current.IsPointerOverGameObject())
        {
            DragMouseMove();
            ZoomMouseMove();
        }
        
    }
    private void DragMouseMove()
    {
        if (Input.GetMouseButton(0))
        {
            xmove += Input.GetAxis("Mouse X") * movespeed;
        }
        transform.position = new Vector3(xmove, limitY, limitZ);
        MoveLimit();
    }
    void MoveLimit()
    {
        Vector3 temp;
        temp.x = Mathf.Clamp(transform.position.x, -verticalPosition, verticalPosition);
        temp.y = limitY;
        temp.z = limitZ;

        transform.position = temp;
    }
    private void ZoomMouseMove()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            cam.fieldOfView = Mathf.Clamp(cam.fieldOfView -= zoomValue * wheelspeed, minZoom, maxZoom);
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            cam.fieldOfView = Mathf.Clamp(cam.fieldOfView += zoomValue * wheelspeed, minZoom, maxZoom);
        }
    }

}