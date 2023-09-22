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

    [SerializeField] private ModelSelectionVisualizer m_ModelSelectionVisualizer;

    [SerializeField] private ARPlacementInteractable m_ARPlacementInteractable;

    public List<ModelData> Models => m_Models;

    private void Start()
    {
        foreach (var modelData in m_Models)
        {
            var model = modelData.Model;
            // Add selection visualizer
            if (model.GetComponentInChildren<ModelSelectionVisualizer>() == null)
            {
                _ = Instantiate(m_ModelSelectionVisualizer, model.transform);
            }

            // Add AR interactions
            if (model.GetComponent<ARSelectionInteractable>() == null)
            {
                model.AddComponent<ARSelectionInteractable>();
                var arTranslationInteractable = model.AddComponent<ARTranslationInteractable>();
                arTranslationInteractable.objectGestureTranslationMode = GestureTransformationUtility.GestureTranslationMode.Any;
                model.AddComponent<ARRotationInteractable>();
                model.AddComponent<ARScaleInteractable>();
            }
        }

        m_ARPlacementInteractable.placementPrefab = Models[0].Model;
        TutorialManager.OnGalleryStarted?.Invoke();
    }

    public void OnModelSelected(int idx)
    {
        m_ARPlacementInteractable.placementPrefab = Models[idx].Model;
    }

    public void OnStartToPlaceObject()
    {
        m_ARPlacementInteractable.gameObject.SetActive(true);
    }

    public void OnObjectPlaced()
    {
        m_ARPlacementInteractable.gameObject.SetActive(false);
        TutorialManager.OnObjectAdded?.Invoke();
    }
}
