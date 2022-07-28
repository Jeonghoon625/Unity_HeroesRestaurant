using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    /// <summary>
    /// �ش� ��ũ��Ʈ�� ������ ī�޶� �߰�
    /// ������ Screen Match Mode �� Expand�� �������
    /// </summary>
    private void Awake()
    {
        Screen.SetResolution(1920, 1080, true);

        Screen.SetResolution(Screen.width, (Screen.width * 9) / 16, true);

        //Camera cam = GetComponent<Camera>();

        //// ī�޶� ������Ʈ�� Viewport Rect
        //Rect rt = cam.rect;

        //// ���� ���� ��� 9:16, �ݴ�� �ϰ� ������ 16:9�� �Է�.
        //float scale_height = ((float)Screen.width / Screen.height) / ((float)16 / 9); // (���� / ����)
        //float scale_width = 1f / scale_height;
        //if (scale_height < 1)
        //{
        //    rt.height = scale_height;
        //    rt.y = (1f - scale_height) / 2f;
        //}
        //else
        //{
        //    rt.width = scale_width;
        //    rt.x = (1f - scale_width) / 2f;
        //}

        //cam.rect = rt;




        ////Default �ػ� ����
        //float fixedAspectRatio = 9f / 16f;

        ////���� �ػ��� ����
        //float currentAspectRatio = (float)Screen.width / (float)Screen.height;

        ////���� �ػ� ���� ������ �� �� ���
        //if (currentAspectRatio > fixedAspectRatio) thisCanvas.matchWidthOrHeight = 0;
        ////���� �ػ��� ���� ������ �� �� ���
        //else if (currentAspectRatio < fixedAspectRatio) thisCanvas.matchWidthOrHeight = 1;
    }
    //��ó: https://ssscool.tistory.com/408 [����:Ƽ���丮]
}
