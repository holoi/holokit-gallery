using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AR;

[Serializable]
public struct ModelData
{
    public Sprite Image;

    public GameObject Model;
}

public class GalleryManager : MonoBehaviour
{
    [SerializeField] private List<ModelData> m_Models;

    [SerializeField] private ARPlacementInteractable m_ARPlacementInteractable;

    public List<ModelData> Models => m_Models;

    private void Start()
    {
        foreach (var modelData in m_Models)
        {
            var model = modelData.Model;
            model.AddComponent<ARSelectionInteractable>();
            model.AddComponent<ARTranslationInteractable>();
            model.AddComponent<ARRotationInteractable>();
            model.AddComponent<ARScaleInteractable>();
        }

        m_ARPlacementInteractable.placementPrefab = Models[0].Model;
    }

    public void OnModelSelected(int idx)
    {
        m_ARPlacementInteractable.placementPrefab = Models[idx].Model;
    }
}
