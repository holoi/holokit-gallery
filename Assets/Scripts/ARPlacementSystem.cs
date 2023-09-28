using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Lean.Touch;

public class ARPlacementSystem : MonoBehaviour
{
    [SerializeField] private GameObject m_PlacementIndicator;

    [SerializeField] private Camera m_MainCamera;

    private ARRaycastManager m_ARRaycastManager;

    private Pose m_PlacementPose;

    private bool m_IsPlacementPoseValid;

    private float m_LastEnabledTime;

    public event Action<Vector3, Quaternion> OnPlaced;

    private void OnEnable()
    {
        LeanTouch.OnFingerTap += HandleFingerTap;
        m_LastEnabledTime = Time.time;
    }

    private void OnDisable()
    {
        LeanTouch.OnFingerTap -= HandleFingerTap;
    }

    private void Start()
    {
        m_ARRaycastManager = FindObjectOfType<ARRaycastManager>();
    }

    private void Update()
    {
        UpdatePlacementPose();
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = m_MainCamera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        m_ARRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        m_IsPlacementPoseValid = hits.Count > 0;
        m_PlacementIndicator.SetActive(m_IsPlacementPoseValid);
        if (m_IsPlacementPoseValid)
        {
            // Update placement pose
            m_PlacementPose = hits[0].pose;
            var cameraForward = m_MainCamera.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0f, cameraForward.z).normalized;
            m_PlacementPose.rotation = Quaternion.LookRotation(cameraBearing);

            // Update placement indicator
            m_PlacementIndicator.transform.SetPositionAndRotation(m_PlacementPose.position, m_PlacementPose.rotation);
        }
    }

    private void HandleFingerTap(LeanFinger finger)
    {
        if (Time.time - m_LastEnabledTime < 0.1f)
            return;

        if (finger.IsOverGui)
            return;

#if !UNITY_EDITOR
        if (m_IsPlacementPoseValid)
            OnPlaced?.Invoke(m_PlacementPose.position, m_PlacementPose.rotation);
#else
        OnPlaced?.Invoke(new Vector3(0f, 0f, 5f), Quaternion.identity);
#endif
    }
}
