using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    /// <summary>
    /// 해당 스크립트를 각각의 카메라에 추가
    /// 에디터 Screen Match Mode 를 Expand로 해줘야함
    /// </summary>
    private void Awake()
    {
        Screen.SetResolution(1920, 1080, true);

        Screen.SetResolution(Screen.width, (Screen.width * 9) / 16, true);

        //Camera cam = GetComponent<Camera>();

        //// 카메라 컴포넌트의 Viewport Rect
        //Rect rt = cam.rect;

        //// 현재 세로 모드 9:16, 반대로 하고 싶으면 16:9를 입력.
        //float scale_height = ((float)Screen.width / Screen.height) / ((float)16 / 9); // (가로 / 세로)
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




        ////Default 해상도 비율
        //float fixedAspectRatio = 9f / 16f;

        ////현재 해상도의 비율
        //float currentAspectRatio = (float)Screen.width / (float)Screen.height;

        ////현재 해상도 가로 비율이 더 길 경우
        //if (currentAspectRatio > fixedAspectRatio) thisCanvas.matchWidthOrHeight = 0;
        ////현재 해상도의 세로 비율이 더 길 경우
        //else if (currentAspectRatio < fixedAspectRatio) thisCanvas.matchWidthOrHeight = 1;
    }
    //출처: https://ssscool.tistory.com/408 [시작:티스토리]
}
