using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HoloInteractive.XR.HoloKit;
using System;

public class ModelSelectorUIController : MonoBehaviour
{
    [SerializeField] private GalleryManager m_GalleryManager;

    [SerializeField] private GameObject m_ModelSlotPrefab;

    [SerializeField] private RectTransform m_Root;

    private HoloKitCameraManager m_HoloKitCameraManager;

    private void Start()
    {
        int idx = 0;
        foreach (var modelData in m_GalleryManager.Models)
        {
            var modelSlot = Instantiate(m_ModelSlotPrefab, m_Root);
            int i = idx;
            modelSlot.GetComponent<Button>().onClick.AddListener(() => {
                m_GalleryManager.OnModelSelected(i);
                modelSlot.GetComponent<ModelSlotController>().OnSelected();
            });
            if (modelData.Image != null)
                modelSlot.GetComponent<Image>().sprite = modelData.Image;

            if (idx == 0)
            {
                m_GalleryManager.OnModelSelected(0);
                modelSlot.GetComponent<ModelSlotController>().OnSelected();
            }

            idx++;
        }

        m_HoloKitCameraManager = FindObjectOfType<HoloKitCameraManager>();
        m_HoloKitCameraManager.OnScreenRenderModeChanged += OnScreenRenderModeChanged;
    }

    private void Update()
    {
        if (m_HoloKitCameraManager.ScreenRenderMode == ScreenRenderMode.Mono)
        {
            if (Screen.orientation != ScreenOrientation.Portrait)
            {
                Screen.orientation = ScreenOrientation.Portrait;
            }
        }
    }

    private void OnScreenRenderModeChanged(ScreenRenderMode renderMode)
    {
        gameObject.SetActive(renderMode == ScreenRenderMode.Mono);
    }
}
