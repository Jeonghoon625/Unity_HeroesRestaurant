using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Camera mainCamera;
    Vector3 cameraPos;

    [SerializeField]
    [Range(0f, 0.1f)]
    float shakeRange = 0.05f;
    [SerializeField]
    [Range(0f, 1f)]
    float duration = 0.5f;

    private void Awake()
    {
        mainCamera = Camera.main;
    }
    public void Shake()
    {
        cameraPos = mainCamera.transform.position;
        InvokeRepeating("StartShake", 0f, 0.005f);
        Invoke("StopShake", duration);
    }

    public void StartShake()
    {
        var cameraPosX = Random.value * shakeRange * 2 - shakeRange;
        var cameraPosY = Random.value * shakeRange * 2 - shakeRange;
        Vector3 cameraPos = mainCamera.transform.position;
        cameraPos.x += cameraPosX;
        cameraPos.y += cameraPosY;
        mainCamera.transform.position = cameraPos;
    }

    public void StopShake()
    {
        CancelInvoke("StartShake");
        mainCamera.transform.position = cameraPos;
    }
}
