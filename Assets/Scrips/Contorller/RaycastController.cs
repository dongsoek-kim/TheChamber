using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RaycastController
{
    public delegate void RaycastResultDelegate(RaycastHit hit);

    private static Camera mainCamera => Camera.main;

    /// <summary>
    /// 플레이어의 에임에 들어온 오브젝트를 판별하기위한 메서드
    /// 전역에서 사용
    /// </summary>
    /// <param name="distance"></param>
    /// <param name="onHit"></param>
    public static void ProcessRaycast(float distance, RaycastResultDelegate onHit)
    {
        Camera camera = mainCamera;
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distance))
        {
            onHit?.Invoke(hit);
        }
    }
}
