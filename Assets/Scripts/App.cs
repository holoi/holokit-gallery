using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lean.Touch;
using Lean.Common;
using HoloInteractive.XR.HoloKit;

public class App : MonoBehaviour
{
    [SerializeField] private ModelLibrary m_ModelLibrary;

    [SerializeField] private ModelSelectionVisualizer m_ModelSelectionVisualizerPrefab;

    [Header("References")]
    [SerializeField] private ARPlacementSystem m_ARPlacementSystem;

    [SerializeField] private LeanSelectByFinger m_LeanSelectByFinger;

    [Header("UI")]
    [SerializeField] private GameObject m_UIPanel;

    [SerializeField] private GameObject m_OperationPanel;

    [SerializeField] private GameObject m_SelectionPanel;

    [SerializeField] private Button m_CancelButton;

    [SerializeField] private Button m_RestoreButton;

    public ModelLibrary ModelLibrary => m_ModelLibrary;

    private int m_SelectedModelIndex = 0;

    private HoloKitCameraManager m_HoloKitCameraManager;

    public event Action OnAddButtonPressed;

    public event Action OnObjectPlaced;

    private void Start()
    {
        m_ARPlacementSystem.OnPlaced += OnPlaced;

        m_ARPlacementSystem.gameObject.SetActive(false);

        m_HoloKitCameraManager = FindObjectOfType<HoloKitCameraManager>();
        m_HoloKitCameraManager.OnScreenRenderModeChanged += OnScreenRenderModeChanged;
    }

    private void Update()
    {
        if (m_HoloKitCameraManager.ScreenRenderMode == ScreenRenderMode.Mono)
            Screen.orientation = ScreenOrientation.Portrait;

        if (m_LeanSelectByFinger.Selectables.Count > 0)
        {
            var selectable = m_LeanSelectByFinger.Selectables[0];
            var modelController = selectable.GetComponent<ModelController>();
            if (selectable.transform.localScale == modelController.InitialScale)
            {
                m_RestoreButton.interactable = false;
            }
            else
            {
                m_RestoreButton.interactable = true;
            }
        }
        else
        {
            m_RestoreButton.interactable = false;
        }
    }

    private void OnScreenRenderModeChanged(ScreenRenderMode renderMode)
    {
        m_UIPanel.SetActive(renderMode == ScreenRenderMode.Mono);
    }

    public void OnStartAddingModel()
    {
        // Turn on ARPlacementSystem
        m_ARPlacementSystem.gameObject.SetActive(true);


        // Switch UI panels
        m_OperationPanel.SetActive(false);
        m_SelectionPanel.SetActive(true);

        OnAddButtonPressed?.Invoke();
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

        OnObjectPlaced?.Invoke();
    }

    public void OnModelSelected(int idx)
    {
        m_SelectedModelIndex = idx;
    }

    public void OnDeleteModel()
    {
        if (m_LeanSelectByFinger.Selectables.Count > 0)
        {
            var selectable = m_LeanSelectByFinger.Selectables[0];
            Destroy(selectable.gameObject);
        }
    }

    public void OnRestoreModel()
    {
        if (m_LeanSelectByFinger.Selectables.Count > 0)
        {
            var selectable = m_LeanSelectByFinger.Selectables[0];
            var modelController = selectable.GetComponent<ModelController>();
            selectable.transform.localScale = modelController.InitialScale;
        }
    }

    public void OnSelected(LeanSelectable selectable)
    {
        m_CancelButton.interactable = true;
    }

    public void OnDeselected(LeanSelectable selectable)
    {
        if (m_LeanSelectByFinger.Selectables.Count == 0)
        {
            m_CancelButton.interactable = false;
            m_RestoreButton.interactable = false;
        }
    }
}
