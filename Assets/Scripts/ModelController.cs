using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;
using Lean.Common;

[RequireComponent(typeof(LeanSelectableByFinger))]
[RequireComponent(typeof(LeanDragTranslate))]
[RequireComponent(typeof(LeanTwistRotateAxis))]
[RequireComponent(typeof(LeanPinchScale))]
public class ModelController : MonoBehaviour
{
    private ModelSelectionVisualizer m_SelectionVisualizer;

    private float m_InitialYPos;

    private void Awake()
    {
        var leanSelectableByFinger = GetComponent<LeanSelectableByFinger>();
        leanSelectableByFinger.OnSelected.AddListener(OnSelected);
        leanSelectableByFinger.OnDeselected.AddListener(OnDeselected);

        var leanDragTranslate = GetComponent<LeanDragTranslate>();
        leanDragTranslate.Use.RequiredSelectable = leanSelectableByFinger;
        leanDragTranslate.Camera = Camera.main;

        var leanTwistRotateAxis = GetComponent<LeanTwistRotateAxis>();
        leanTwistRotateAxis.Use.RequiredSelectable = leanSelectableByFinger;

        var leanPinchScale = GetComponent<LeanPinchScale>();
        leanPinchScale.Use.RequiredSelectable = leanSelectableByFinger;
        leanPinchScale.Camera = Camera.main;

        m_SelectionVisualizer = GetComponentInChildren<ModelSelectionVisualizer>();

        m_InitialYPos = transform.position.y;
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, m_InitialYPos, transform.position.z);
    }

    private void OnSelected(LeanSelect select)
    {
        if (m_SelectionVisualizer != null)
            m_SelectionVisualizer.gameObject.SetActive(true);
        Debug.Log($"OnSelected: {gameObject.name}");
    }

    private void OnDeselected(LeanSelect select)
    {
        if (m_SelectionVisualizer != null)
            m_SelectionVisualizer.gameObject.SetActive(false);
        Debug.Log($"OnDeselected: {gameObject.name}");
    }
}
