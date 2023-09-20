using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlacementManager : MonoBehaviour
{
    [SerializeField] Camera m_MainCamera;

    [SerializeField] GameObject m_PlacementIndicator;

    [SerializeField] GalleryManager m_GalleryManager;

    [SerializeField] RectTransform m_InteractionArea;

    private ARRaycastManager m_ARRaycastManager;

    private Pose m_PlacementPose;

    private bool m_PlacementPoseIsValid = false;

    private void Start()
    {
        m_ARRaycastManager = FindObjectOfType<ARRaycastManager>();
    }

    private void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();

        if (m_PlacementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            var touch = Input.GetTouch(0);
            if (touch.position.y > Screen.height * 0.2f)
            {
                m_GalleryManager.InstantiateSelectedModel(m_PlacementPose.position, Quaternion.Euler(0f, 180f, 0f) * m_PlacementPose.rotation);
                gameObject.SetActive(false);
            }
        }
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = m_MainCamera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        m_ARRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        m_PlacementPoseIsValid = hits.Count > 0;
        if (m_PlacementPoseIsValid)
        {
            m_PlacementPose = hits[0].pose;

            var cameraForward = m_MainCamera.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0f, cameraForward.z).normalized;
            m_PlacementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }

    private void UpdatePlacementIndicator()
    {
        m_PlacementIndicator.SetActive(m_PlacementPoseIsValid);

        if (m_PlacementPoseIsValid)
        {
            m_PlacementIndicator.transform.SetPositionAndRotation(m_PlacementPose.position, m_PlacementPose.rotation);
        }
    }
}
