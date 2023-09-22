using System;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject m_AddObjectTip;

    [SerializeField] private GameObject m_TapToPlaceObjectTip;

    [SerializeField] private GameObject m_DragToTranslateObjectTip;

    [SerializeField] private GameObject m_TwistToRotateObjectTip;

    [SerializeField] private GameObject m_PinchToScaleObjectTip;

    private GameObject m_CurrentTip;

    public static Action OnGalleryStarted;

    public static Action OnAddButtonPressed;

    public static Action OnObjectAdded;

    public static Action OnDragGestureStarted;

    public static Action OnDragGestureFinished;

    public static Action OnTwistGestureStarted;

    public static Action OnTwistGestureFinished;

    public static Action OnPinchGestureStarted;

    public static Action OnPinchGestureFinished;

    public void EnableTutorial(bool enabled)
    {
        m_CurrentTip.SetActive(enabled);
    }

    private void Awake()
    {
        OnGalleryStarted += OnGalleryStartedFunc;
        OnAddButtonPressed += OnAddButtonPressedFunc;
        OnObjectAdded += OnObjectAddedFunc;
        OnDragGestureStarted += OnDragGestureStartedFunc;
        OnDragGestureFinished += OnDragGestureFinishedFunc;
        OnTwistGestureStarted += OnTwistGestureStartedFunc;
        OnTwistGestureFinished += OnTwistGestureFinishedFunc;
        OnPinchGestureStarted += OnPinchGestureStartedFunc;
        OnPinchGestureFinished += OnPinchGestureFinishedFunc;
    }

    private void SwitchTip(GameObject nextTip)
    {
        if (m_CurrentTip != null)
            m_CurrentTip.SetActive(false);

        m_CurrentTip = nextTip;
        m_CurrentTip.SetActive(true);
    }

    private void OnGalleryStartedFunc()
    {
        SwitchTip(m_AddObjectTip);
    }

    private void OnAddButtonPressedFunc()
    {
        SwitchTip(m_TapToPlaceObjectTip);
    }

    private void OnObjectAddedFunc()
    {
        SwitchTip(m_DragToTranslateObjectTip);
    }

    private void OnDragGestureStartedFunc()
    {
        m_CurrentTip.SetActive(false);
    }

    private void OnDragGestureFinishedFunc()
    {
        SwitchTip(m_TwistToRotateObjectTip);
    }

    private void OnTwistGestureStartedFunc()
    {
        m_CurrentTip.SetActive(false);
    }

    private void OnTwistGestureFinishedFunc()
    {
        SwitchTip(m_PinchToScaleObjectTip);
    }

    private void OnPinchGestureStartedFunc()
    {
        m_CurrentTip.SetActive(false);
    }

    private void OnPinchGestureFinishedFunc()
    {
        m_CurrentTip.SetActive(false);
    }
}
