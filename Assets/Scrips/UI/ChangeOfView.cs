using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum View
{
    Fps,
    Tps
}

public class ChangeOfView : MonoBehaviour
{
    public Camera Fps;
    public Camera Tps;

    private View currentView = View.Fps;
    public static event Action<View> OnViewChanged;
    private void UpdateCameraState()
    {
        switch (currentView)
        {
            case View.Fps:
                Fps.gameObject.SetActive(true);
                Tps.gameObject.SetActive(false);
                break;
            case View.Tps:
                Fps.gameObject.SetActive(false);
                Tps.gameObject.SetActive(true);
                break;
        }
    }

    public void OnChangeOfView()
    {
        currentView = (currentView == View.Fps) ? View.Tps : View.Fps;
        UpdateCameraState();
        OnViewChanged?.Invoke(currentView);
    }
}

