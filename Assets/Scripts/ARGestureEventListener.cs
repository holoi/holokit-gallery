using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AR;

public class ARGestureEventListener : MonoBehaviour
{
    [SerializeField] private ARGestureInteractor m_ARGestureInteractor;

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
            TutorialManager.OnDragGestureStarted?.Invoke();
        };

        gesture.onUpdated += (DragGesture g) =>
        {

        };

        gesture.onFinished += (DragGesture g) =>
        {
            TutorialManager.OnDragGestureFinished?.Invoke();
        };
    }

    private void OnTwistGestureStarted(TwistGesture gesture)
    {
        gesture.onStart += (TwistGesture g) =>
        {
            TutorialManager.OnTwistGestureStarted?.Invoke();              
        };

        gesture.onUpdated += (TwistGesture g) =>
        {

        };

        gesture.onFinished += (TwistGesture g) =>
        {
            TutorialManager.OnTwistGestureFinished?.Invoke();
        };
    }

    private void OnPinchGestureStarted(PinchGesture gesture)
    {
        gesture.onStart += (PinchGesture g) =>
        {
            TutorialManager.OnPinchGestureStarted?.Invoke();
        };

        gesture.onUpdated += (PinchGesture g) =>
        {

        };

        gesture.onFinished += (PinchGesture g) =>
        {
            TutorialManager.OnPinchGestureFinished?.Invoke();
        };
    }
}
