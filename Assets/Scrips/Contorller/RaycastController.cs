using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RaycastController
{
    public delegate void RaycastResultDelegate(RaycastHit hit);

    private static Camera mainCamera => Camera.main;

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
