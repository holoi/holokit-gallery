using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AR;

public class ARGestureEventListener : MonoBehaviour
{
    [SerializeField] private ARGestureInteractor m_ARGestureInteractor;

    private bool m_DragGestureStarted = false;
    private bool m_DragGestureFinished = false;
    private bool m_TwistGestureStarted = false;
    private bool m_TwistGestureFinished = false;
    private bool m_PinchGestureStarted = false;
    private bool m_PinchGestureFinished = false;

    private void Start()
    {
        m_ARGestureInteractor.dragGestureRecognizer.onGestureStarted += OnDragGestureStarted;
        m_ARGestureInteractor.twistGestureRecognizer.onGestureStarted += OnTwistGestureStarted;
        m_ARGestureInteractor.pinchGestureRecognizer.onGestureStarted += OnPinchGestureStarted;
    }

    private void OnDragGestureStarted(DragGesture gesture)
    {
        gesture.onStart += (DragGesture g) =>
        {
            Debug.Log("OnDragGestureStarted onStart");
            if (!m_DragGestureStarted)
            {
                m_DragGestureStarted = true;
                TutorialManager.OnDragGestureStarted?.Invoke();
            }
        };

        gesture.onUpdated += (DragGesture g) =>
        {

        };

        gesture.onFinished += (DragGesture g) =>
        {
            Debug.Log("OnDragGestureStarted onFinished");
            if (!m_DragGestureFinished)
            {
                m_DragGestureFinished = true;
                TutorialManager.OnDragGestureFinished?.Invoke();
            }
        };
    }

    private void OnTwistGestureStarted(TwistGesture gesture)
    {
        gesture.onStart += (TwistGesture g) =>
        {
            Debug.Log("OnTwistGestureStarted onStart");
            if (!m_TwistGestureStarted)
            {
                m_TwistGestureStarted = true;
                TutorialManager.OnTwistGestureStarted?.Invoke();              
            }
        };

        gesture.onUpdated += (TwistGesture g) =>
        {

        };

        gesture.onFinished += (TwistGesture g) =>
        {
            Debug.Log("OnTwistGestureStarted onFinished");
            if (!m_TwistGestureFinished)
            {
                m_TwistGestureFinished = true;
                TutorialManager.OnTwistGestureFinished?.Invoke();
            }
        };
    }

    private void OnPinchGestureStarted(PinchGesture gesture)
    {
        gesture.onStart += (PinchGesture g) =>
        {
            Debug.Log("OnPinchGestureStarted onStart");
            if (!m_PinchGestureStarted)
            {
                m_PinchGestureStarted = true;
                TutorialManager.OnPinchGestureStarted?.Invoke();
            }
        };

        gesture.onUpdated += (PinchGesture g) =>
        {

        };

        gesture.onFinished += (PinchGesture g) =>
        {
            Debug.Log("OnPinchGestureStarted onFinished");
            if (!m_PinchGestureFinished)
            {
                m_PinchGestureFinished = true;
                TutorialManager.OnPinchGestureFinished?.Invoke();
            }
        };
    }
}
