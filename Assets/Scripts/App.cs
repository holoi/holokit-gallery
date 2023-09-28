using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

public class App : MonoBehaviour
{
    [SerializeField] private ModelLibrary m_ModelLibrary;

    [SerializeField] private ModelSelectionVisualizer m_ModelSelectionVisualizerPrefab;

    [Header("References")]
    [SerializeField] private ARPlacementSystem m_ARPlacementSystem;

    [SerializeField] private GameObject m_LeanTouch;

    [SerializeField] private LeanSelectByFinger m_LeanSelectByFinger;

    [Header("UI")]
    [SerializeField] private GameObject m_OperationPanel;

    [SerializeField] private GameObject m_SelectionPanel;

    public ModelLibrary ModelLibrary => m_ModelLibrary;

    private int m_SelectedModelIndex = 0;

    private void Start()
    {
        m_ARPlacementSystem.OnPlaced += OnPlaced;

        m_ARPlacementSystem.gameObject.SetActive(false);
    }

    public void OnStartAddingModel()
    {
        // Turn on ARPlacementSystem
        m_ARPlacementSystem.gameObject.SetActive(true);


        // Switch UI panels
        m_OperationPanel.SetActive(false);
        m_SelectionPanel.SetActive(true);
    }

    public void OnPlaced(Vector3 position, Quaternion rotation)
    {
        Debug.Log($"[App] OnPlaced");
        // Turn off ARPlacementSystem
        m_ARPlacementSystem.gameObject.SetActive(false);

        // Place the model
        var modelInstance = Instantiate(m_ModelLibrary.Models[m_SelectedModelIndex].Mesh, position, rotation);
        Instantiate(m_ModelSelectionVisualizerPrefab, modelInstance.transform);
        modelInstance.AddComponent<ModelController>();

        var selectable = modelInstance.GetComponent<LeanSelectableByFinger>();
        m_LeanSelectByFinger.Select(selectable);

        // Switch UI panels
        m_OperationPanel.SetActive(true);
        m_SelectionPanel.SetActive(false);
    }

    public void OnModelSelected(int idx)
    {
        m_SelectedModelIndex = idx;
    }
}
