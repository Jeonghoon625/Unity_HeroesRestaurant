using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMoveDrag : MonoBehaviour
{
    public static float xmove = 0f;
    private float movespeed = 0.005f;
    private float wheelspeed = 0.2f;

    private float limitY = 0.02f;
    private float limitZ = -0.1f;

    private float verticalPosition = 0.38f;

    private Camera cam;

    int minZoom = 20;
    int maxZoom = 45;
    int zoomValue = 25;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }
    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                DragMouseMove();
            }
        }
        else if (Input.GetMouseButton(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                DragMouseMove();
            }
        }
        ZoomMouseMove();
    }

    public void DragMouseMove()
    {
        //if (Input.GetMouseButton(0))
        {
            xmove -= Input.GetAxis("Mouse X") * movespeed;
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