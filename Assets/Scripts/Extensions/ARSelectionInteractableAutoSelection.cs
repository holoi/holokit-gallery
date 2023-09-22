using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.AR;

public class ARSelectionInteractableAutoSelection : ARSelectionInteractable
{
    public bool m_GestureSelected { get; private set; } = true;

    public override bool IsSelectableBy(IXRSelectInteractor interactor)
    {
        return interactor is ARGestureInteractor;
    }
}
