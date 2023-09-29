using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Lean.Touch;

public class LeanARPlaneDrag : MonoBehaviour
{
    public LeanSelectableByFinger LeanSelectableByFinger
    {
        set
        {
            m_LeanSelectableByFinger = value;
        }
    }

    [SerializeField] private LeanSelectableByFinger m_LeanSelectableByFinger;

    [SerializeField] private float m_LeapSpeed = 5f;

    private ARRaycastManager m_ARRaycastManager;

    private bool m_IsDragging = false;

    private void Start()
    {
        m_ARRaycastManager = FindObjectOfType<ARRaycastManager>();
    }

    private void Update()
    {
        if (!m_LeanSelectableByFinger.IsSelected)
            return;

        if (LeanTouch.Fingers.Count == 1)
        {
            var finger = LeanTouch.Fingers[0];
            // First frame
            if (finger.Down)
            {
                Ray ray = Camera.main.ScreenPointToRay(finger.ScreenPosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.transform.TryGetComponent<ModelController>(out var _))
                    {
                        m_IsDragging = true;
                    }
                }
            }
            else if (finger.Up)
            {
                m_IsDragging = false;
            }

            if (m_IsDragging)
            {
                List<ARRaycastHit> hits = new();
                if (m_ARRaycastManager.Raycast(finger.ScreenPosition, hits, TrackableType.PlaneWithinPolygon))
                {
                    transform.position = Vector3.Lerp(transform.position, hits[0].pose.position, m_LeapSpeed * Time.deltaTime);
                }
            }
        }
        else
        {
            m_IsDragging = false;
        }
    }
}
