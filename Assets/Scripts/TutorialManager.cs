using System;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject m_AddObjectTip;

    [SerializeField] private GameObject m_TapToPlaceObjectTip;

    [SerializeField] private GameObject m_DragToTranslateObjectTip;

    [SerializeField] private GameObject m_TwistToRotateObjectTip;

    [SerializeField] private GameObject m_PinchToScaleObjectTip;

    private GameObject m_CurrentTip;

    private bool m_TutorialEnabled = true;

    private bool m_FirstTransform = true;

    private const float FADE_DURATION = 0.5f;

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
        m_TutorialEnabled = enabled;
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

    private void Start()
    {
        m_CurrentTip = m_AddObjectTip;
        m_CurrentTip.SetActive(true);
    }

    private void SwitchTip(GameObject nextTip)
    {
        //if (m_CurrentTip != null)
        //    m_CurrentTip.SetActive(false);

        //m_CurrentTip = nextTip;
        //if (m_TutorialEnabled)
        //    m_CurrentTip.SetActive(true);

        var images = m_CurrentTip.GetComponentsInChildren<Image>();
        LeanTween.value(gameObject, 1f, 0f, FADE_DURATION)
            .setOnUpdate((float alpha) =>
            {
                foreach (var image in images)
                {
                    Color color = image.color;
                    color.a = alpha;
                    image.color = color;
                }
            })
            .setOnComplete(() =>
            {
                m_CurrentTip.SetActive(false);
                m_CurrentTip = nextTip;
                m_CurrentTip.SetActive(true);

                var newImages = m_CurrentTip.GetComponentsInChildren<Image>();
                LeanTween.value(gameObject, 0f, 1f, FADE_DURATION)
                    .setOnUpdate((float alpha) =>
                    {
                        foreach (var image in newImages)
                        {
                            Color color = image.color;
                            color.a = alpha;
                            image.color = color;
                        }
                    });
            });
    }

    private void OnGalleryStartedFunc()
    {
        //SwitchTip(m_AddObjectTip);
    }

    private void OnAddButtonPressedFunc()
    {
        SwitchTip(m_TapToPlaceObjectTip);
    }

    private void OnObjectAddedFunc()
    {
        if (m_FirstTransform)
        {
            m_FirstTransform = false;
            SwitchTip(m_DragToTranslateObjectTip);
        }
    }

    private void OnDragGestureStartedFunc()
    {
        m_CurrentTip.SetActive(false);
    }

    private void OnDragGestureFinishedFunc()
    {
        if (m_CurrentTip == m_DragToTranslateObjectTip)
        {
            SwitchTip(m_TwistToRotateObjectTip);
        } 
    }

    private void OnTwistGestureStartedFunc()
    {
        m_CurrentTip.SetActive(false);
    }

    private void OnTwistGestureFinishedFunc()
    {
        if (m_CurrentTip == m_TwistToRotateObjectTip)
            SwitchTip(m_PinchToScaleObjectTip);
    }

    private void OnPinchGestureStartedFunc()
    {
        m_CurrentTip.SetActive(false);
    }

    private void OnPinchGestureFinishedFunc()
    {
        Debug.Log("OnPinchGestureFinishedFunc");
        m_CurrentTip.SetActive(false);
    }
}
