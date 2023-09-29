using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;
using Lean.Common;

[RequireComponent(typeof(LeanSelectableByFinger))]
//[RequireComponent(typeof(LeanDragTranslate))]
[RequireComponent(typeof(LeanARPlaneDrag))]
[RequireComponent(typeof(LeanTwistRotateAxis))]
[RequireComponent(typeof(LeanPinchScale))]
public class ModelController : MonoBehaviour
{
    public Vector3 InitialScale => m_InitialScale;

    private ModelSelectionVisualizer m_SelectionVisualizer;

    private float m_InitialYPos;

    private Vector3 m_InitialScale;

    private void Awake()
    {
        var leanSelectableByFinger = GetComponent<LeanSelectableByFinger>();
        leanSelectableByFinger.OnSelected.AddListener(OnSelected);
        leanSelectableByFinger.OnDeselected.AddListener(OnDeselected);

        //var leanDragTranslate = GetComponent<LeanDragTranslate>();
        //leanDragTranslate.Use.RequiredSelectable = leanSelectableByFinger;
        //leanDragTranslate.Camera = Camera.main;
        var leanARPlaneDrag = GetComponent<LeanARPlaneDrag>();
        leanARPlaneDrag.LeanSelectableByFinger = leanSelectableByFinger;

        var leanTwistRotateAxis = GetComponent<LeanTwistRotateAxis>();
        leanTwistRotateAxis.Use.RequiredSelectable = leanSelectableByFinger;

        var leanPinchScale = GetComponent<LeanPinchScale>();
        leanPinchScale.Use.RequiredSelectable = leanSelectableByFinger;
        leanPinchScale.Camera = Camera.main;

        m_SelectionVisualizer = GetComponentInChildren<ModelSelectionVisualizer>();

        m_InitialYPos = transform.position.y;
        m_InitialScale = transform.localScale;
        Debug.Log($"{gameObject.name} with initial scale: {m_InitialScale}");
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, m_InitialYPos, transform.position.z);
    }

    private void OnSelected(LeanSelect select)
    {
        if (m_SelectionVisualizer != null)
            m_SelectionVisualizer.gameObject.SetActive(true);
    }

    private void OnDeselected(LeanSelect select)
    {
        if (m_SelectionVisualizer != null)
            m_SelectionVisualizer.gameObject.SetActive(false);
    }
}
