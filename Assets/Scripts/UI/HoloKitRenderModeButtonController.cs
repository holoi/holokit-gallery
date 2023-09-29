using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HoloInteractive.XR.HoloKit;

public class HoloKitRenderModeButtonController : MonoBehaviour
{
    [SerializeField] private Sprite m_StereoImage;

    [SerializeField] private Sprite m_MonoImage;

    [SerializeField] private Image m_Image;

    private void Start()
    {
        var holokitCameraManager = FindObjectOfType<HoloKitCameraManager>();
        holokitCameraManager.OnScreenRenderModeChanged += OnScreenRenderModeChanged;
    }

    private void OnScreenRenderModeChanged(ScreenRenderMode renderMode)
    {
        if (renderMode == ScreenRenderMode.Stereo)
        {
            m_Image.sprite = m_MonoImage;
        }
        else
        {
            m_Image.sprite = m_StereoImage;
        }
    }
}
